export type Preticate<T> = (e: T) => boolean;

export default class ListUtil {
  public static removeElement<T>(
    list: T[],
    selector: T | Preticate<T>
  ): boolean {
    let index;
    if (typeof selector === 'function')
      index = list.findIndex((e) => (selector as Preticate<T>)(e));
    else index = list.indexOf(selector as T);
    if (index >= 0) {
      list.splice(index, 1);
      return true;
    }
    return false;
  }
}
