import Header from "@/components/layout/header";
import Sidebar from "@/components/layout/sidebar";
import { Box } from "@mui/material";

export default function MainLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <>
      {/* Fixed Header */}
      <Header />

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