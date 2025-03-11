export interface LoginReq {
    userId?: string;
    password?: string;
}

export interface LoginRes {
    userId?: string;
    userName?: string;
    role?: string;
    accessToken?: string;
}