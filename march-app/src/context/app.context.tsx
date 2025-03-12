"use client";
import { createContext, useState, useEffect, ReactNode, useContext } from "react";
import { ThemeProvider, createTheme } from "@mui/material/styles";
import CssBaseline from "@mui/material/CssBaseline";
import { getStorage, setStorage } from "@/utils/storage.utils";

type ThemeMode = 'light' | 'dark';
// type ThemeColor = "blue" | "red" | "green" | "purple";

type ContextStore = {
  fontSize: number;
  themeMode: ThemeMode;
  collapseMenu: boolean;
};

const KEY_APP_CONTEXT: string = 'appContext';

interface AppContextProps {
  themeMode: ThemeMode;
  toggleTheme: () => void;
  fontSize: number;
  changeFontSize: (size: number | number[]) => void;
  collapseMenu: boolean;
  toggleMenuCollapse: () => void;
}

export const AppContext = createContext<AppContextProps | null | undefined>(undefined);

export function AppContextProvider({ children }: { children: ReactNode }) {
  const [themeMode, setThemeMode] = useState<ThemeMode>('light');
  const [collapseMenu, setCollapseMenu] = useState(false);
  const [fontSize, setFontSize] = useState(14);

  useEffect(() => {
    let store: ContextStore | null = getStorage<ContextStore>(KEY_APP_CONTEXT);
    if (store) {
      setThemeMode(store.themeMode);
      changeFontSize(store.fontSize);
      setCollapseMenu(store.collapseMenu);
    } else {
      store = {
        fontSize, collapseMenu, themeMode
      };
      setStorage(KEY_APP_CONTEXT, store);
    }

  }, [collapseMenu, fontSize, themeMode]);

  const toggleTheme = () => {
    const store: ContextStore | null = getStorage<ContextStore>(KEY_APP_CONTEXT);
    if (store) {
      const newThemeMode = store.themeMode === "light" ? "dark" : "light"; // ✅ Correct toggle logic
      setThemeMode(newThemeMode); // ✅ Update state properly
      setStorage(KEY_APP_CONTEXT, { ...store, themeMode: newThemeMode }); // ✅ Store updated value
    }
  };

  const changeFontSize = (size: number | number[]) => {
    const store: ContextStore | null = getStorage<ContextStore>(KEY_APP_CONTEXT);
    if (store) {
      const newFontSize = Array.isArray(size) ? size[0] ?? 14 : size; // ✅ Ensure valid number
      setFontSize(newFontSize); // ✅ Update React state
      setStorage(KEY_APP_CONTEXT, { ...store, fontSize: newFontSize }); // ✅ Update storage
    }
  };

  const toggleMenuCollapse = () => {
    const store: ContextStore | null = getStorage<ContextStore>(KEY_APP_CONTEXT);
    if (store) {
      const newCollapseMenu = !store.collapseMenu; // ✅ Ensure valid number
      setCollapseMenu(newCollapseMenu); // ✅ Update React state
      setStorage(KEY_APP_CONTEXT, { ...store, collapseMenu: newCollapseMenu }); // ✅ Update storage
    }
  };

  const theme = createTheme({
    palette: {
      mode: themeMode,
    },
    typography: {
      fontSize: fontSize, // Apply the dynamic font size
    },
  });

  return (
    <AppContext.Provider value={{ themeMode, toggleTheme, fontSize: fontSize, changeFontSize, collapseMenu, toggleMenuCollapse }}>
      <ThemeProvider theme={theme}>
        <CssBaseline />
        {children}
      </ThemeProvider>
    </AppContext.Provider>
  );
}

export const useAppContext = () => useContext(AppContext);