import NextAuth from "next-auth"
import Credentials from "next-auth/providers/credentials"

export const { handlers, signIn, signOut, auth } = NextAuth({
    providers: [
        Credentials({
            // You can specify which fields should be submitted, by adding keys to the `credentials` object.
            credentials: {
                userId: {},
                password: {},
            },
            //   authorize: async (credentials) => {
            authorize: async () => {
                const user = null

                // // logic to verify if the user exists
                // user = await getUserFromDb(credentials.email, credentials.password)

                // if (!user) {
                //   // No user found, so this is their first attempt to login
                //   // Optionally, this is also the place you could do a user registration
                //   throw new Error("Invalid credentials.")
                // }

                // return user object with their profile data
                return user
            },
        }),
    ],
})