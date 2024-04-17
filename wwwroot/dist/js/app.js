"use strict";
window.setItem = (key, value) => {
    localStorage.setItem(key, value);
};
window.getItem = (key) => {
    return localStorage.getItem(key);
};
