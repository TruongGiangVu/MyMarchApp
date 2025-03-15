import { JSX } from "react";

export type NavItem = {
  title: string;
  icon?: JSX.Element;
  path?: string;
  children?: NavItem[];
};