export default class LocalStorageUtil {
  public static get<T>(key: string, def?: T): T | null {
    let data = window.localStorage.getItem(key);
    if (!data) return def ?? null;
    return (JSON.parse(data) as T) ?? def ?? null;
  }

  public static set<T>(key: string, val: T) {
    let data = JSON.stringify(val);
    window.localStorage.setItem(key, data);
  }
}
