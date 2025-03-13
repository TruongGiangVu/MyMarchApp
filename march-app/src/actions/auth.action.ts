'use server';

import { signIn } from "@/auth";
import { ApiResponse } from "@/types";
import { AuthError } from "next-auth";

export async function authenticate(userId: string, password: string): Promise<ApiResponse> {
    try {
        const res = await signIn("credentials", {
            userId: userId,
            password: password,
            redirect: false,
        })
        return {
            isSuccess: true,
            code: '00',
            message: 'Thành công',
            details: [res],
        };
    } catch (error) {
        if (error instanceof AuthError) {
            return {
                isSuccess: false,
                code: '99',
                message: error.message ?? '',
            };
        } else return {
            isSuccess: false,
            code: '99',
            message: 'Lỗi không xác định',
        };

    }
}