import { signIn } from "@/auth";

export async function authenticate(userId: string, password: string) {
    try {
        const res = await signIn("credentials", {
            userId: userId,
            password: password,
            redirect: false,
        })
        console.log(">>> check r: ", res);
        return res;
    } catch (error) {
        console.error(error);
    }
}