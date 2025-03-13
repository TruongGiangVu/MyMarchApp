'use server';

import { signIn } from "@/auth";

export async function authenticate(userId: string, password: string) {
    try {
        // const res = await signIn("credentials", {
        //     userId: userId,
        //     password: password,
        //     redirect: false,
        // })
        await new Promise((resolve) => setTimeout(resolve, 3000));
        const res = { isSuccess: true, message: null };
        console.log(">>> check r: ", res);
        return res;
    } catch (error) {
        console.error(error);
    }
}