export default class UtilityService {
  FormatDateString(date?: Date): string {
    if (!date) return "N/A";
    let dateString = date.toString();

    try {
      // DateTimeFormatOptions
      const date: Date = new Date(dateString);
      const options: Intl.DateTimeFormatOptions = {
        year: "numeric",
        month: "long",
        day: "numeric",
      };
      const formattedDate: string = new Intl.DateTimeFormat(
        "en-US",
        options
      ).format(date);
      dateString = formattedDate;
    } catch (error) {
      console.log(error);
      console.log(date);
      console.log(
        "Error while converting date string " + date + " exception " + error
      );
    }

    return dateString;
  }

  FormQueryParametersFromCriteria<T>(criterias: T): string {
    let query: string = "?";

    if (criterias) {
      const queryString = Object.entries(criterias as object)
        .filter(([key, value]) => value !== undefined)
        .map(([key, value]) => `Filters.${key}=${value}`)
        .join("&");

      return query + queryString;
    }

    return query;
  }
}
