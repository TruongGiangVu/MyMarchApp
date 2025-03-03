import { ThemeProvider } from "@/context/theme.context"
import Header from "./header"
import Sidebar from "./sidebar"
import { Box } from "@mui/material"

export default function MainLayout ({
    children,
  }: Readonly<{
    children: React.ReactNode;
  }>)  {
    return (
        <ThemeProvider>
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
        </ThemeProvider>
    )
}