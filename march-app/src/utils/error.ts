import { AuthError } from "next-auth";

export class CustomAuthError extends AuthError {
  constructor(message: string) {
    super(message); // No "Read more" link appended
    this.name = "CustomAuthError";
    this.message = this.message.replace(/ Read more at https:\/\/errors\.authjs\.dev#autherror$/, "");
  }
}