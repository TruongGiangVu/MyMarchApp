"use client";

import { Box } from "@mui/material";
import Header from "./header";
import Sidebar from "./sidebar";
import { useAppContext } from "@/context/app.context";
import { useSession } from "next-auth/react"
import { HEADER_RATIO } from "@/core";

export default function MainLayoutComponent({
    children,
}: Readonly<{
    children: React.ReactNode;
}>) {
    const { fontSize } = useAppContext()!;
    const { data: session } = useSession()
    return (
        <>
            {/* Fixed Header */}
            <Header user={session?.user} fontSize={fontSize} />

            {/* Sidebar + Main Content Wrapper */}
            <Box sx={{ display: "flex", height: `calc(100vh - ${fontSize * HEADER_RATIO}px)`, mt: `${fontSize * HEADER_RATIO}px` }}>
                <Sidebar />

                {/* Main Content */}
                <Box component="main" sx={{ flexGrow: 1, p: 3, overflow: "auto" }}>
                    {children}
                </Box>
            </Box>
        </>
    )
}