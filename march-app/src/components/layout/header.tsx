"use client";

import { AppBar, Toolbar, IconButton, Typography } from "@mui/material";
import MenuIcon from "@mui/icons-material/Menu";
import MenuOpenIcon from '@mui/icons-material/MenuOpen';
import { useAppContext } from "@/context/app.context";

export default function Header() {
  const { collapseMenu, toggleMenuCollapse: toggleSidebar } = useAppContext()!;
  return (
    <AppBar position="fixed" enableColorOnDark>
      <Toolbar>
        <IconButton edge="start" color="inherit" onClick={toggleSidebar}>
          {collapseMenu ? <MenuOpenIcon /> : <MenuIcon />}
        </IconButton>
        <Typography variant="h6" sx={{ flexGrow: 1 }}>
          Dashboard
        </Typography>
        <div>User name</div>
      </Toolbar>
    </AppBar>
  );
}
