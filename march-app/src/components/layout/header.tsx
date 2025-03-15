import { AppBar, Toolbar, IconButton, Typography, Button } from "@mui/material";
import MenuIcon from "@mui/icons-material/Menu";
import MenuOpenIcon from '@mui/icons-material/MenuOpen';
import { useAppContext } from "@/context/app.context";
import { User } from "next-auth";
import { signOut } from "next-auth/react";
import PowerSettingsNewIcon from '@mui/icons-material/PowerSettingsNew';
import { HEADER_PADDING_RATIO, HEADER_RATIO } from "@/core";

interface IProps {
  user?: User;
  fontSize: number;
}

export default function Header({ user, fontSize }: IProps) {
  const { collapseMenu, toggleMenuCollapse: toggleSidebar } = useAppContext()!;
  return (
    <AppBar position="fixed" enableColorOnDark sx={{ height: `${fontSize * HEADER_RATIO}px` }}>
      <Toolbar sx={{ minHeight: `${fontSize * HEADER_RATIO}px !important`, paddingX: `${fontSize * HEADER_PADDING_RATIO}px !important` }}>
        <IconButton edge="start" color="inherit" onClick={toggleSidebar}>
          {collapseMenu ? <MenuOpenIcon /> : <MenuIcon />}
        </IconButton>
        <Typography variant="h6" sx={{ flexGrow: 1 }}>
          Dashboard
        </Typography>

        {user && (<>
          <div>{user?.userName}</div>
          <Button startIcon={<PowerSettingsNewIcon />} size="small" sx={{ marginLeft: 1 }} variant="outlined" color="inherit" onClick={() => signOut()}>Đăng xuất</Button>
        </>)}

      </Toolbar>
    </AppBar>
  );
}
