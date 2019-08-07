#pragma once

#include <windows.h>

extern HWND  win32_window;
extern float win32_scroll;

bool win32_init(const char *app_name);
void win32_shutdown();
void win32_step_begin();
void win32_step_end();