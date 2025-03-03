export interface ApiResponse {
    isSuccess: boolean;
    code: string;
    message: string;
    details?: string[] | null;
}

export interface ApiResPayload<T> extends ApiResponse {
    payload?: T | null;
}