/**
 * Collection of utility functions to access and
 * save to local storage.
 */
export default class LocalStorageUtil {
  /**
   * Tries to retive the value of the given key from
   * local storage and pases it to the given type.
   * Returns def ot null if the key could not be
   * retrieved.
   * @param key
   * @param def
   */
  public static get<T>(key: string, def?: T): T | null {
    let data = window.localStorage.getItem(key);
    if (!data) return def ?? null;
    return (JSON.parse(data) as T) ?? def ?? null;
  }

  /**
   * Sets the given value to the given key to local
   * storage parsed to a JSON string.
   * @param key
   * @param val
   */
  public static set<T>(key: string, val: T) {
    let data = JSON.stringify(val);
    window.localStorage.setItem(key, data);
  }
}
