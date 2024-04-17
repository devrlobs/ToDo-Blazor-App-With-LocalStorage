window.setItem = (key: string, value: string) => {
    localStorage.setItem(key, value);
};
window.getItem = (key: string) => {
    return localStorage.getItem(key);
};