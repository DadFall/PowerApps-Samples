import * as React from "react";
import { CellEditorOverrides } from "../types";

export const cellEditorOverrides: CellEditorOverrides = {
	["Text"]: (props, col) => {
		// 待办： Add your custom cell editor overrides here
		return null;
	},
};
