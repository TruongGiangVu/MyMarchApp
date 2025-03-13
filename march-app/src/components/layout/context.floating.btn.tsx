'use client';

import React, { useState } from "react";
import { Fab, Drawer, Box, FormControl, Slider, IconButton, Stack, Button } from "@mui/material";
import SettingsIcon from "@mui/icons-material/Settings";
import { ThemeColor, themeColors, useAppContext } from "@/context/app.context";
import Brightness4Icon from "@mui/icons-material/Brightness4";
import Brightness7Icon from "@mui/icons-material/Brightness7";


export default function ContextFloatingBtn() {
  const { themeMode, toggleTheme, themeColor, changeThemeColor, fontSize, changeFontSize, resetToDefault } = useAppContext()!;
  const [open, setOpen] = useState(false);

  return (
    <>
      {/* Floating Action Button */}
      <Fab
        color="primary"
        sx={{ position: "fixed", bottom: 24, left: 24, zIndex: 10000 }} // Adjust the position
        onClick={() => setOpen(true)}
      >
        <SettingsIcon />
      </Fab>

      {/* Drawer for Theme Settings */}
      <Drawer anchor="left" open={open} onClose={() => setOpen(false)}>
        <Box sx={{ width: 250, padding: 2 }}>
          <h3>Thay đổi chủ đề</h3>

          {/* Theme Mode Toggle */}
          <Box>
            <IconButton onClick={toggleTheme} color="inherit">
              {themeMode === 'dark' ? <Brightness7Icon /> : <Brightness4Icon />}
            </IconButton>
            <label>Chế độ {themeMode === 'dark' ? 'sáng' : 'tối'}</label>
          </Box>

          {/* Theme Color Selection */}
          <FormControl fullWidth sx={{ mt: 2 }}>
            <label>Màu chủ đề</label>
            <Stack direction="row" spacing={2}>
              {Object.keys(themeColors).map((color) => (
                <Button
                  key={color}
                  size="small"
                  variant="contained"
                  onClick={() => changeThemeColor(color as ThemeColor)}
                  sx={{
                    backgroundColor: themeColors[color as ThemeColor].primary.main,
                    width: 32,
                    height: 32,
                    minWidth: 32,
                    borderRadius: "50%",
                    border: themeColor === color ? "2px solid black" : "none",
                  }}
                />
              ))}
            </Stack>
          </FormControl>

          {/* Font Size Selection */}
          <Box sx={{ mt: 2 }}>
            <label>Kích thước</label>
            <Slider
              value={fontSize}
              min={10}
              max={18}
              step={1}
              valueLabelDisplay="auto"
              onChange={(e, newValue) => changeFontSize(newValue as number)}
            />
          </Box>

          {/* Reset To Default Selection */}
          <Box sx={{ mt: 2 }}>
            <Button
              fullWidth
              variant="outlined"
              sx={{ mt: 3, textTransform: "none" }}
              onClick={resetToDefault}
            >
              Đặt lại mặc định
            </Button>
          </Box>
        </Box>
      </Drawer>
    </>
  );
}
