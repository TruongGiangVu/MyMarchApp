"use client";
import { AppBar, Toolbar, IconButton, Typography, Slider } from "@mui/material";
import MenuIcon from "@mui/icons-material/Menu";
import Brightness4Icon from "@mui/icons-material/Brightness4";
import Brightness7Icon from "@mui/icons-material/Brightness7";
import { useAppContext } from "@/context/app.context";

export default function Header() {
  const appContext = useAppContext();

  return (
    <AppBar position="fixed">
      <Toolbar>
        <IconButton edge="start" color="inherit">
          <MenuIcon />
        </IconButton>
        <Typography variant="h6" sx={{ flexGrow: 1 }}>
          Dashboard
        </Typography>
        <Slider
          min={10}
          max={18}
          value={appContext?.fontSize}
          onChange={(_e, newValue) => appContext?.changeFontSize(newValue)}
          valueLabelDisplay="auto"
        />
        <IconButton onClick={appContext?.toggleTheme} color="inherit">
          {appContext?.themeMode === 'dark' ? <Brightness7Icon /> : <Brightness4Icon />}
        </IconButton>
      </Toolbar>
    </AppBar>
  );
}
