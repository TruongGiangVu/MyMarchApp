"use client";

import { JSX, useState } from "react";
import { Drawer, List, ListItemButton, ListItemIcon, ListItemText, Collapse, Box } from "@mui/material";
import { ExpandLess, ExpandMore } from "@mui/icons-material";
import DashboardIcon from "@mui/icons-material/Dashboard";
import SettingsIcon from "@mui/icons-material/Settings";
import Link from "next/link";
import { useAppContext } from "@/context/app.context";

type NavItem = {
  title: string;
  icon?: JSX.Element;
  path?: string;
  children?: NavItem[];
};

const navigation: NavItem[] = [
  { title: "Dashboard", icon: <DashboardIcon />, path: "/" },
  {
    title: "Management",
    icon: <SettingsIcon />,
    children: [
      {
        title: "Users",
        children: [
          { title: "Active Users", path: "/management/users/active" },
          { title: "Banned Users", path: "/management/users/banned" },
        ],
      },
      { title: "Reports", path: "/management/reports" },
    ],
  },
];

export default function Sidebar() {
  const { collapseMenu } = useAppContext()!;
  const [openItems, setOpenItems] = useState<{ [key: string]: boolean }>({});

  const toggleItem = (title: string) => {
    setOpenItems((prev) => ({ ...prev, [title]: !prev[title] }));
  };

  // Recursive rendering function
  const renderNavItems = (items: NavItem[], level = 0) => {
    return items.map((item) => (
      <div key={item.title}>
        <ListItemButton
          onClick={() => item.children ? toggleItem(item.title) : null}
          component={item.path ? Link : "div"}
          href={item.path || "#"}
          sx={{ pl: level * 2 }}
        >
          {item.icon && <ListItemIcon>{item.icon}</ListItemIcon>}
          {!collapseMenu && <ListItemText primary={item.title} />}
          {item.children && !collapseMenu && (openItems[item.title] ? <ExpandLess /> : <ExpandMore />)}
        </ListItemButton>
        {item.children && !collapseMenu && (
          <Collapse in={openItems[item.title]} timeout="auto" unmountOnExit>
            <List component="div" disablePadding>
              {renderNavItems(item.children, level + 1)}
            </List>
          </Collapse>
        )}
      </div>
    ));
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
      </Box>
      <List>{renderNavItems(navigation)}</List>
    </Drawer>
  );
}