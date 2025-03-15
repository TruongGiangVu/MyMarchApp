"use client";

import { useState } from "react";
import { Drawer, List, ListItemButton, ListItemIcon, ListItemText, Collapse, Tooltip } from "@mui/material";
import { ExpandLess, ExpandMore } from "@mui/icons-material";

import Link from "next/link";
import { usePathname } from "next/navigation";
import { useAppContext } from "@/context/app.context";
import { COLLAPSE_MENU_RATIO, EXPANDED_MENU_RATIO, HEADER_RATIO } from "@/core";
import { NavItem } from "@/types";
import { navigationItems } from "./data/navigation";

export default function Sidebar() {
  const { collapseMenu, fontSize } = useAppContext()!;
  const pathname = usePathname(); // Get current path for active link styling
  const [openItems, setOpenItems] = useState<{ [key: string]: boolean }>({});

  const toggleItem = (title: string) => {
    setOpenItems((prev) => ({ ...prev, [title]: !prev[title] }));
  };

  const isActive = (path?: string) => path && pathname.startsWith(path);

  const renderNavItems = (items: NavItem[], level = 0) => {
    return items.map((item) => {
      const active = isActive(item.path);
      return (
        <div key={item.title}>
          <Tooltip title={collapseMenu ? item.title : ""} placement="right" arrow>
            <ListItemButton
              onClick={() => !collapseMenu && item.children ? toggleItem(item.title) : null}
              component={item.path ? Link : "div"}
              href={item.path || "#"}
              sx={{
                pl: collapseMenu ? 2 : level * 2,
                justifyContent: "center",
                backgroundColor: active ? "primary.light" : "transparent",
                "&:hover": {
                  backgroundColor: "primary.light",
                },
              }}

            >
              {item.icon && (
                <ListItemIcon sx={{ minWidth: 40, color: active ? "primary.main" : "inherit" }}>
                  {item.icon}
                </ListItemIcon>
              )}
              {!collapseMenu && <ListItemText primary={item.title} />}
              {!collapseMenu && item.children && (openItems[item.title] ? <ExpandLess /> : <ExpandMore />)}
            </ListItemButton>
          </Tooltip>

          {!collapseMenu && item.children && (
            <Collapse in={openItems[item.title]} timeout="auto" unmountOnExit>
              <List component="div" disablePadding>
                {renderNavItems(item.children, level + 1)}
              </List>
            </Collapse>
          )}
        </div>
      );
    });
  };

  return (
    <>
      <Drawer
        variant="permanent"
        sx={{
          width: collapseMenu ? fontSize * COLLAPSE_MENU_RATIO : fontSize * EXPANDED_MENU_RATIO,
          flexShrink: 0,
          "& .MuiDrawer-paper": {
            width: collapseMenu ? fontSize * COLLAPSE_MENU_RATIO : fontSize * EXPANDED_MENU_RATIO,
            transition: "width 0.3s",
            overflowX: "hidden",
            mt: `${fontSize * HEADER_RATIO}px`,
          },
        }}
      >
        <List>{renderNavItems(navigationItems)}</List>
      </Drawer>
    </>
  );
}
