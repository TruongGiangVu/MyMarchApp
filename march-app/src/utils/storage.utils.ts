// Save an object to localStorage
export const setStorage = <T>(key: string, value: T): void => {
    try {
        localStorage.setItem(key, JSON.stringify(value));
    } catch (error) {
        console.error("Error saving to localStorage", error);
    }
};

// Get an object from localStorage
export const getStorage = <T>(key: string): T | null => {
    try {
        const item = localStorage.getItem(key);
        return item ? (JSON.parse(item) as T) : null;
    } catch (error) {
        console.error("Error reading from localStorage", error);
        return null;
    }
};

// Remove an item from localStorage
export const removeStorage = (key: string): void => {
    localStorage.removeItem(key);
};


// const user = { name: "John Doe", age: 30 };
// setStorage("user", user);
// const storedUser = getStorage<{ name: string; age: number }>("user");
// console.log(storedUser?.name); // "John Doe"
// removeStorage("user");
