"use client";
import { useState } from "react";
import { Drawer, List, ListItemButton, ListItemIcon, ListItemText, Collapse, IconButton, Box } from "@mui/material";
import { ExpandLess, ExpandMore, Menu } from "@mui/icons-material";
import DashboardIcon from "@mui/icons-material/Dashboard";
import SettingsIcon from "@mui/icons-material/Settings";
import Link from "next/link";
import { useAppContext } from "@/context/app.context";


const navigation = [
  { title: "Dashboard", icon: <DashboardIcon />, path: "/" },
  {
    title: "Management",
    icon: <SettingsIcon />,
    children: [
      { title: "Users", path: "/management/users" },
      { title: "Reports", path: "/management/reports" },
    ],
  },
];

export default function Sidebar() {
  const {collapseMenu, setCollapseMenu} = useAppContext()!;
  const [openItems, setOpenItems] = useState<{ [key: string]: boolean }>({});

  const toggleSidebar = () => setCollapseMenu(!collapseMenu);
  const toggleItem = (title: string) => {
    setOpenItems((prev) => ({ ...prev, [title]: !prev[title] }));
  };

  return (
    <Drawer
      variant="permanent"
      sx={{
        width: !collapseMenu ? 240 : 64,
        flexShrink: 0,
        "& .MuiDrawer-paper": {
          width: !collapseMenu ? 240 : 64,
          transition: "width 0.3s",
          overflowX: "hidden",
          mt: "64px", // Push below header
        },
      }}
    >
      <Box sx={{ display: "flex", justifyContent: "center", p: 1 }}>
        <IconButton onClick={toggleSidebar}>
          <Menu />
        </IconButton>
      </Box>
      <List>
        {navigation.map((item) => (
          <div key={item.title}>
            <ListItemButton onClick={() => item.children ? toggleItem(item.title) : null} component={Link} href={item.path || "#"}>
              <ListItemIcon>{item.icon}</ListItemIcon>
              {!collapseMenu && <ListItemText primary={item.title} />}
              {item.children && !collapseMenu && (openItems[item.title] ? <ExpandLess /> : <ExpandMore />)}
            </ListItemButton>
            {item.children && !collapseMenu && (
              <Collapse in={openItems[item.title]} timeout="auto" unmountOnExit>
                <List component="div" disablePadding>
                  {item.children.map((child) => (
                    <ListItemButton key={child.title} component={Link} href={child.path} sx={{ pl: 4 }}>
                      <ListItemText primary={child.title} />
                    </ListItemButton>
                  ))}
                </List>
              </Collapse>
            )}
          </div>
        ))}
      </List>
    </Drawer>
  );
}