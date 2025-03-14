/* eslint-disable @typescript-eslint/no-unused-vars */
import NextAuth from "next-auth";

declare module "next-auth" {
  interface User {
    id: string; // NextAuth expects an `id` field
    userId?: string;
    userName?: string;
    role?: string;
    accessToken?: string;
  }

  interface Session {
    user: User
  }
}

declare module "next-auth/jwt" {
  /** Returned by the `jwt` callback and `getToken`, when using JWT sessions */
  interface JWT {
    user: User;
  }
}
