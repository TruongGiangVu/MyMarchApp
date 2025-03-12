"use client";

import { createContext, useState, ReactNode, useContext, useEffect, useMemo } from "react";
import { ThemeProvider, createTheme } from "@mui/material/styles";
import CssBaseline from "@mui/material/CssBaseline";
import { initializeStorage, setStorage } from "@/utils/storage.utils";
import { APP_ALIAS } from "@/core";

type ThemeMode = 'light' | 'dark';
export type ThemeColor = "default" | "green" | "orange";

type ContextStore = {
  fontSize: number;
  themeMode: ThemeMode;
  themeColor: ThemeColor;
  collapseMenu: boolean;
};

export const themeColors: Record<ThemeColor, { primary: { main: string }; secondary: { main: string } }> = {
  default: { primary: { main: "#1976d2" }, secondary: { main: "#dc004e" } }, // MUI default colors
  green: { primary: { main: "#4CAF50" }, secondary: { main: "#8BC34A" } },
  orange: { primary: { main: "#FF9800" }, secondary: { main: "#FF5722" } }
};

const KEY_APP_CONTEXT: string = `${APP_ALIAS}-context`;
const DEFAULT_VALUES: ContextStore = { fontSize: 14, themeMode: "light", themeColor: "default", collapseMenu: false };

interface AppContextProps {
  themeMode: ThemeMode;
  toggleTheme: () => void;
  themeColor: ThemeColor;
  changeThemeColor: (color: ThemeColor) => void;
  fontSize: number;
  changeFontSize: (size: number | number[]) => void;
  collapseMenu: boolean;
  toggleMenuCollapse: () => void;
  resetToDefault: () => void;
}

export const AppContext = createContext<AppContextProps | null | undefined>(undefined);

export function AppContextProvider({ children }: { children: ReactNode }) {
  const [mounted, setMounted] = useState(false);
  const [themeMode, setThemeMode] = useState<ThemeMode>("light");
  const [themeColor, setThemeColor] = useState<ThemeColor>("default");
  const [collapseMenu, setCollapseMenu] = useState(false);
  const [fontSize, setFontSize] = useState(14);

  useEffect(() => {
    // Only run on client
    const storedContext = initializeStorage(KEY_APP_CONTEXT, DEFAULT_VALUES);
    setThemeMode(storedContext.themeMode);
    setThemeColor(storedContext.themeColor);
    setCollapseMenu(storedContext.collapseMenu);
    setFontSize(storedContext.fontSize);
    setMounted(true); // ✅ set mount true
  }, []);

  const updateStorage = (updatedValues: Partial<ContextStore>) => {
    setStorage(KEY_APP_CONTEXT, { themeMode, themeColor, collapseMenu, fontSize, ...updatedValues });
  };

  const toggleTheme = () => {
    setThemeMode((prev) => {
      const newThemeMode = prev === "light" ? "dark" : "light";
      updateStorage({ themeMode: newThemeMode });
      return newThemeMode;
    });
  };

  const changeThemeColor = (color: ThemeColor) => {
    setThemeColor(color);
    updateStorage({ themeColor: color });
  };

  const changeFontSize = (size: number | number[]) => {
    const newFontSize = Array.isArray(size) ? size[0] ?? DEFAULT_VALUES.fontSize : size;
    setFontSize(newFontSize);
    updateStorage({ fontSize: newFontSize });
  };

  const toggleMenuCollapse = () => {
    setCollapseMenu((prev) => {
      const newCollapseState = !prev;
      updateStorage({ collapseMenu: newCollapseState });
      return newCollapseState;
    });
  };

  const resetToDefault = () => {
    setThemeMode(DEFAULT_VALUES.themeMode);
    setThemeColor(DEFAULT_VALUES.themeColor);
    setFontSize(DEFAULT_VALUES.fontSize);
    setCollapseMenu(DEFAULT_VALUES.collapseMenu);

    updateStorage(DEFAULT_VALUES);
  };

  // Memoize theme to prevent re-renders
  const theme = useMemo(() => {
    // Ensure themeColor is always valid
    const selectedTheme = themeColors[themeColor] || themeColors["default"];

    return createTheme({
      palette: {
        mode: themeMode,
        primary: selectedTheme.primary,
        secondary: selectedTheme.secondary
      },
      typography: { fontSize }
    });
  }, [themeMode, themeColor, fontSize]);

  // Prevent rendering until mounted to avoid hydration issues
  if (!mounted) return null;

  return (
    <AppContext.Provider value={{ themeMode, toggleTheme, themeColor, changeThemeColor, fontSize, changeFontSize, collapseMenu, toggleMenuCollapse, resetToDefault }}>
      <ThemeProvider theme={theme}>
        <CssBaseline />
        {children}
      </ThemeProvider>
    </AppContext.Provider>
  );
}

export const useAppContext = () => useContext(AppContext);