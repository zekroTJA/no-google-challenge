/**
 * Preticate function type which takes an
 * element and the index of the and element
 * and returns a boolean.
 */
export type Preticate<T> = (e: T, i: number) => boolean;

/**
 * Collection of utility functions
 * for list objects.
 */
export default class ListUtil {
  /**
   * Removes a given element or an element selected
   * by a given preticate function from the given
   * list reference.
   * @param list
   * @param selector
   */
  public static removeElement<T>(
    list: T[],
    selector: T | Preticate<T>
  ): boolean {
    let index;
    if (typeof selector === 'function')
      index = list.findIndex((e, i) => (selector as Preticate<T>)(e, i));
    else index = list.indexOf(selector as T);
    if (index >= 0) {
      list.splice(index, 1);
      return true;
    }
    return false;
  }
}
