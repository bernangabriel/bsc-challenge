const set = (key: string, value: string) => {
  localStorage.setItem(key, value);
};

const get = (key: string): string | null => {
  return localStorage.getItem(key);
};

const remove = (key: string) => {
  localStorage.removeItem(key);
};

export default {
  set,
  get,
  remove,
};
