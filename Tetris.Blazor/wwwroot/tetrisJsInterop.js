// This file is to show how a library package may provide JavaScript interop features
// wrapped in a .NET API

var TetrisComponent = TetrisComponent || {}
TetrisComponent.SetFocusToElement = (element) => {
    element.focus();
};
