/** Save an object to Storage with json format */
export const setStorage = <T>(key: string, value: T): void => {
    try {
        localStorage.setItem(key, JSON.stringify(value));
    } catch (error) {
        console.error("Error saving to localStorage", error);
    }
};
// Ex: const user = { name: "John Doe", age: 30 }; setStorage("user", user);

/** Get an object from storage, could be null */
export const getStorage = <T>(key: string): T | null => {
    try {
        const item = localStorage.getItem(key);
        return item ? (JSON.parse(item) as T) : null;
    } catch (error) {
        console.error("Error reading from storage", error);
        return null;
    }
};
// Ex: const storedUser = getStorage<{ name: string; age: number }>("user");

/** Remove an item from storage */
export const removeStorage = (key: string): void => {
    localStorage.removeItem(key);
};
// Ex: removeStorage("user");

/** Store default value if there is no key in storage */
export const initializeStorage = <T>(key: string, defaultValue: T): T => {
    const stored = getStorage<T>(key);
    if (stored === null || stored === undefined) {
        setStorage(key, defaultValue); // Store default if no existing value
        return defaultValue;
    }
    return stored;
};