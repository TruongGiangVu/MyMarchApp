import { auth } from "@/auth";
import { Routes } from "@/core";
import { HttpMethod, QueryParams, LogOption, NextOption, PrimaryType, ResponseType } from "@/types";
import { redirect } from "next/navigation";


interface ApiOptions {
    method?: HttpMethod;
    body?: unknown;
    responseType?: ResponseType;
    queryParams?: QueryParams;
    headers?: Record<string, string>;
    log?: LogOption;
    nextOption?: NextOption;
    autoAddAuth?: boolean; // Make this optional
    fullUrl?: boolean; // Option to override baseUrl if needed
}

const defaultApiOptions: Partial<ApiOptions> = {
    method: 'GET',
    responseType: "json",
    log: "none",
    fullUrl: false,
    autoAddAuth: true,
};

const BASE_URL = process.env.MARCH_API || ""; // Set base URL

/**
 * Encode query parameters for a URL.
 */
function encodeQueryParams(params: QueryParams): string {
    const queryParams = new URLSearchParams();

    Object.entries(params).forEach(([key, value]) => {
        if (value === null || value === undefined) return; // Skip null & undefined

        if (Array.isArray(value)) {
            value.forEach((item) => queryParams.append(key, formatValue(item)));
        } else {
            queryParams.append(key, formatValue(value));
        }
    });

    return queryParams.toString();
}

/**
 * Convert different types to string format for URL encoding.
 */
function formatValue(value: PrimaryType): string {
    return value instanceof Date ? value.toISOString() : String(value);
}

/**
 * Get token and userId of sign in user
 */
async function getSession(): Promise<{ token?: string; userId?: string; }> {
    const session = await auth();
    const token = session?.user?.accessToken;
    const userId = session?.user?.userId;
    return { token, userId };
}

/**
 * Generic function to call an API with authentication and logging.
 */
export async function sendApi<T>(
    endpoint: string,
    options: ApiOptions
): Promise<T> {
    const finalOptions = { ...defaultApiOptions, ...options }; // Merge defaults

    const { method, body, responseType, queryParams, headers, log, nextOption, autoAddAuth, fullUrl } = finalOptions;

    // Construct full URL (unless `fullUrl: true` is passed)
    let url = fullUrl ? endpoint : `${BASE_URL}${endpoint}`;

    // Encode query parameters
    if (queryParams) {
        url += `?${encodeQueryParams(queryParams)}`;
    }

    // Get token and userId from Auth.js session
    const { token, userId } = await getSession();

    // Determine if body is FormData (for file uploads)
    const isFormData = body instanceof FormData;

    // Prepare headers
    const headersInit: HeadersInit = {
        ...(isFormData ? {} : { "Content-Type": "application/json" }), // Let browser set headers for FormData
        ...(autoAddAuth && token ? { Authorization: `Bearer ${token}` } : {}),
        ...headers,
    };

    // Prepare request options
    const requestOptions: RequestInit = {
        method,
        headers: headersInit,
        body: body ? (isFormData ? body : JSON.stringify(body)) : undefined,
        ...nextOption,
    };

    // log input
    if (log === "input" || log === "both") {
        console.log("API Request:", userId, { method, url, body });
    }

    try {
        // call api
        const response = await fetch(url, requestOptions);

        // if call api has error
        if (!response.ok) {
            if (response.status === 401) {
                console.warn(`API returned ${response.status}, redirecting to login.`);
                redirect(Routes.SIGN_IN); // Redirect on the server side
            }
            if (response.status === 403) {
                console.warn(`API returned ${response.status}, redirecting to forbidden.`);
                redirect(Routes.FORBIDDEN); // Redirect on the server side
            }

            const errorBody = await response.text();
            throw new Error(`API request failed: ${response.status} ${response.statusText}\n${errorBody}`);
        }

        // set response data type
        let responseData: unknown;
        switch (responseType) {
            case "blob":
                responseData = await response.blob();
                break;
            case "arrayBuffer":
                responseData = await response.arrayBuffer();
                break;
            default:
                responseData = await response.json();
        }

        // log output
        if (log === "output" || log === "both") {
            console.log("API Response:", userId, responseData);
        }

        // return
        return responseData as T;
    } catch (error) {
        console.error("API Error:", userId, error);
        throw error;
    }
}

export default sendApi;

// * GET request with query parameters
// const data = await apiCall<unknown>("https://api.example.com/data", {
//     method: "GET",
//     queryParams: { search: "test", limit: 10 },
//     log: "both",
//   });

// * POST request with JSON body
//   const response = await apiCall<MyResponseType>("https://api.example.com/items", {
//     method: "POST",
//     body: { name: "New Item" },
//     token: "your_jwt_token",
//     log: "input",
//   });

// * POST request with FormData (File Upload)
//   const formData = new FormData();
//   formData.append("file", myFile);
//   formData.append("description", "My uploaded file");
//   const uploadResponse = await apiCall<MyResponseType>("https://api.example.com/upload", {
//     method: "POST",
//     body: formData,
//     token: "your_jwt_token",
//     log: "input",
//   });

// *  GET request that returns a file (Blob)
//   const fileBlob = await apiCall<Blob>("https://api.example.com/download", {
//     method: "GET",
//     token: "your_jwt_token",
//     responseType: "blob",
//   });
//   const url = URL.createObjectURL(fileBlob);
//   const link = document.createElement("a");
//   link.href = url;
//   link.download = "file.pdf";
//   document.body.appendChild(link);
//   link.click();
//   document.body.removeChild(link);

// *  nextOptions
// nextOptions: { next: { tags: ["user-data"], revalidate: 60 } },
