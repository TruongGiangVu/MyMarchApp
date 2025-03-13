import { auth } from "@/auth";
import Header from "@/components/layout/header";
import Sidebar from "@/components/layout/sidebar";
import { Box } from "@mui/material";

export default async function MainLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  const session = await auth();
  return (
    <>
      {/* Fixed Header */}
      <Header user={session?.user}/>

      {/* Sidebar + Main Content Wrapper */}
      <Box sx={{ display: "flex", height: "calc(100vh - 64px)", mt: "64px" }}>
        <Sidebar />

        {/* Main Content */}
        <Box component="main" sx={{ flexGrow: 1, p: 3, overflow: "auto" }}>
          {children}
        </Box>
      </Box>
    </>
  )
}