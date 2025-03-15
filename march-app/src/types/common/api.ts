export type HttpMethod = "GET" | "POST" | "PUT" | "DELETE";

export type LogOption = "input" | "output" | "both" | "none";

export type ResponseType = "json" | "blob" | "arrayBuffer";

export type NextOption = { next?: { revalidate?: number; tags?: string[] } };

export type PrimaryType = string | number | boolean | Date;

export type QueryParams = Record<
    string,
    PrimaryType | PrimaryType[] | null | undefined
>;
