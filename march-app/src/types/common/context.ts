export type ThemeMode = 'light' | 'dark';

export type ThemeColor = "default" | "green" | "orange";

export const themeColors: Record<ThemeColor, { primary: { main: string, light: string; }; secondary: { main: string } }> = {
  default: { primary: { main: "#1976d2", light: "#bbdefb" }, secondary: { main: "#dc004e" } }, // MUI default colors
  green: { primary: { main: "#4CAF50", light: "#a5d6a7" }, secondary: { main: "#8BC34A" } },
  orange: { primary: { main: "#FF9800", light: "#c8e6c9" }, secondary: { main: "#FF5722" } }
};