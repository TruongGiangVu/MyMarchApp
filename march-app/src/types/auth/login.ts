import { z } from "zod";

export interface LoginReq {
    userId?: string | null;
    password?: string | null;
}

export interface LoginRes {
    userId?: string;
    userName?: string;
    role?: string;
    accessToken?: string;
}


export const loginReqValidator = z.object({
    userId: z
        .string()
        .trim()
        .min(1, "Tên đăng nhập không được để trống"),
    password: z
        .string()
        .trim()
        .min(1, "Mật khẩu không được để trống"),
});

export type LoginReqValidator = z.infer<typeof loginReqValidator>;