declare global {
    interface Window {
        setItem: (key: string, value: string) => void;
        getItem: (key: string) => string | null;
    }
}

export { };