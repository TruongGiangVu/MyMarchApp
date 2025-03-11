"use client";
import { createContext, useState, useEffect, ReactNode, useContext } from "react";
import { ThemeProvider as MUIThemeProvider, createTheme } from "@mui/material/styles";
import CssBaseline from "@mui/material/CssBaseline";

// type ThemeMode = 'light' | 'dark';

// type StoreState = {
//   size: number;
//   theme: string;
//   collapseMenu: boolean;
// };

interface AppContextProps {
  darkMode: boolean;
  toggleTheme: () => void;
  size: number;
  changeSize: (size: number) => void;
  collapseMenu: boolean;
  setCollapseMenu: (v: boolean) => void;
}

export const AppContext = createContext<AppContextProps | null | undefined>(undefined);

export function AppContextProvider({ children }: { children: ReactNode }) {
  const [darkMode, setDarkMode] = useState(false);
  const [collapseMenu, setCollapseMenu] = useState(false);
  const [size, setSize] = useState(14);

  useEffect(() => {
    const storedTheme = localStorage.getItem("theme") === "dark";
    setDarkMode(storedTheme);
  }, []);

  const toggleTheme = () => {
    setDarkMode((prev) => {
      localStorage.setItem("theme", prev ? "light" : "dark");
      return !prev;
    });
  };

  const changeSize = (size: number) => {
    setSize(size);
  };

  const theme = createTheme({
    palette: {
      mode: darkMode ? "dark" : "light",
    },
  });

  return (
    <AppContext.Provider value={{ darkMode, toggleTheme, size, changeSize, collapseMenu, setCollapseMenu }}>
      <MUIThemeProvider theme={theme}>
        <CssBaseline />
        {children}
      </MUIThemeProvider>
    </AppContext.Provider>
  );
}

export const useAppContext = () => useContext(AppContext);