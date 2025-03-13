'use client';

import { authenticate } from "@/actions";
import { LoginReq, LoginReqValidator, loginReqValidator } from "@/types";
import { zodResolver } from "@hookform/resolvers/zod";
import { Box, TextField } from "@mui/material";
import { SubmitHandler, useForm } from "react-hook-form";
import LoadingButton from "../buttons/loading.button";

export default function Login() {

    const form = useForm<LoginReqValidator>({
        resolver: zodResolver(loginReqValidator),
        defaultValues: { userId: undefined, password: undefined },
        mode: 'onBlur'
    });

    const {
        register,
        handleSubmit,
        formState: { errors, isSubmitting, isValid },
    } = form;

    const onSubmitForm: SubmitHandler<LoginReqValidator> = async (data: LoginReq) => {
        // call the server action
        console.log(data);
        const res = await authenticate(data.userId ?? '', data.password ?? '');
        console.log(">>> check onSubmitForm: ", res);
    };

    return (
        <>
            <Box component="form" onSubmit={handleSubmit(onSubmitForm)} sx={{ maxWidth: 400, mx: "auto" }}>
                <TextField
                    label="Tên tài khoản"
                    {...register("userId")}
                    error={!!errors.userId?.message}
                    helperText={errors.userId?.message}
                    fullWidth
                    margin="normal"
                />

                <TextField
                    label="Mật khẩu"
                    type="password"
                    {...register("password")}
                    error={!!errors.password?.message}
                    helperText={errors.password?.message}
                    fullWidth
                    margin="normal"
                />

                <LoadingButton type="submit" variant="contained" fullWidth disabled={!isValid} loading={isSubmitting}>Đăng nhập</LoadingButton>
            </Box>
        </>
    )
}