import { Routes } from "@/core";
import { NavItem } from "@/types";
import DashboardIcon from "@mui/icons-material/Dashboard";
import SettingsIcon from "@mui/icons-material/Settings";

export const navigationItems: NavItem[] = [
  { title: "Dashboard", icon: <DashboardIcon />, path: Routes.DASHBOARD },
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