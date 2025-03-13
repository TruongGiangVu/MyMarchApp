"use client";

import { JSX, useState } from "react";
import {
  Drawer,
  List,
  ListItemButton,
  ListItemIcon,
  ListItemText,
  Collapse,
  Box,
  Tooltip,
  Popper,
  Paper,
  ClickAwayListener,
} from "@mui/material";
import { ExpandLess, ExpandMore } from "@mui/icons-material";
import DashboardIcon from "@mui/icons-material/Dashboard";
import SettingsIcon from "@mui/icons-material/Settings";
import Link from "next/link";
import { usePathname } from "next/navigation";
import { useAppContext } from "@/context/app.context";

type NavItem = {
  title: string;
  icon?: JSX.Element;
  path?: string;
  children?: NavItem[];
};

const navigation: NavItem[] = [
  { title: "Dashboard", icon: <DashboardIcon />, path: "/dashboard" },
  {
    title: "Management",
    icon: <SettingsIcon />,
    children: [
      {
        title: "Users",
        icon: <DashboardIcon />,
        children: [
          { title: "Active Users", path: "/management/users/active", icon: <DashboardIcon /> },
          { title: "Banned Users", path: "/management/users/banned", icon: <DashboardIcon /> },
        ],
      },
      {
        title: "Reports",
        icon: <SettingsIcon />,
        children: [
          {
            title: "Monthly Reports",
            path: "/management/reports/monthly",
            icon: <SettingsIcon />,
          },
          {
            title: "Yearly Reports",
            path: "/management/reports/yearly",
            icon: <SettingsIcon />,
          },
        ],
      },
    ],
  },
];

export default function Sidebar() {
  const { collapseMenu } = useAppContext()!;
  const pathname = usePathname(); // Get current path for active link styling
  const [openItems, setOpenItems] = useState<{ [key: string]: boolean }>({});
  const [anchorEls, setAnchorEls] = useState<{ [key: string]: HTMLElement | null }>({});
  const [hoverItem, setHoverItem] = useState<NavItem | null>(null);
  const [hoverLevel, setHoverLevel] = useState<number>(0);
  const [isHoveringPopper, setIsHoveringPopper] = useState(false);

  const toggleItem = (title: string) => {
    setOpenItems((prev) => ({ ...prev, [title]: !prev[title] }));
  };

  const handleMouseEnter = (event: React.MouseEvent<HTMLElement>, item: NavItem, level: number) => {
    if (collapseMenu && item.children) {
      setAnchorEls((prev) => ({ ...prev, [level]: event.currentTarget }));
      setHoverItem(item);
      setHoverLevel(level);
      setIsHoveringPopper(true);
    }
  };

  const handleMouseLeave = () => {
    setTimeout(() => {
      if (!isHoveringPopper) {
        setAnchorEls({});
        setHoverItem(null);
        setHoverLevel(0);
      }
    }, 200);
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
                pl: collapseMenu ? 1 : level * 2,
                justifyContent: "center",
                backgroundColor: active ? "rgba(25, 118, 210, 0.2)" : "transparent",
                "&:hover": {
                  backgroundColor: "rgba(25, 118, 210, 0.1)",
                },
              }}
              onMouseEnter={(e) => handleMouseEnter(e, item, level)}
              onMouseLeave={handleMouseLeave}
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
          width: collapseMenu ? 64 : 240,
          flexShrink: 0,
          "& .MuiDrawer-paper": {
            width: collapseMenu ? 64 : 240,
            transition: "width 0.3s",
            overflowX: "hidden",
            mt: "64px",
          },
        }}
      >
        <Box sx={{ display: "flex", justifyContent: "center", p: 1 }} />
        <List>{renderNavItems(navigation)}</List>
      </Drawer>

      {/* Popper for each nested level */}
      {collapseMenu &&
        Object.entries(anchorEls).map(([level, anchor]) => {
          const item = level == hoverLevel.toString() ? hoverItem : null;
          if (!item || !item.children) return null;

          return (
            <Popper
              key={level}
              open={Boolean(anchor)}
              anchorEl={anchor}
              placement="right-start"
              disablePortal
              modifiers={[
                {
                  name: "preventOverflow",
                  options: { boundary: "window" },
                },
              ]}
            >
              <ClickAwayListener
                onClickAway={(event) => {
                  if (anchor && anchor.contains(event.target as Node)) return;
                  handleMouseLeave();
                }}
              >
                <Paper
                  sx={{
                    p: 1,
                    minWidth: 200,
                    boxShadow: 3,
                    position: "relative",
                    left: `${parseInt(level) * 200}px`, // Move each level further right
                  }}
                  onMouseEnter={() => setIsHoveringPopper(true)}
                  onMouseLeave={() => {
                    setIsHoveringPopper(false);
                    handleMouseLeave();
                  }}
                >
                  <List>
                    {item.children.map((child) => (
                      <ListItemButton
                        key={child.title}
                        component={Link}
                        href={child.path || "#"}
                        sx={{ "&:hover": { backgroundColor: "rgba(25, 118, 210, 0.1)" } }}
                        onClick={() => {
                          setAnchorEls({});
                          setHoverItem(null);
                          setHoverLevel(0);
                        }}
                        onMouseEnter={(e) => child.children && handleMouseEnter(e, child, parseInt(level) + 1)}
                      >
                        {child.icon && <ListItemIcon>{child.icon}</ListItemIcon>}
                        <ListItemText primary={child.title} />
                        {child.children && <ExpandMore />}
                      </ListItemButton>
                    ))}
                  </List>
                </Paper>
              </ClickAwayListener>
            </Popper>
          );
        })}
    </>
  );
}
