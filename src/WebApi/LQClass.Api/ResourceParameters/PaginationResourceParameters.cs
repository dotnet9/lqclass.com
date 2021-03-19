namespace LQClass.Api.ResourceParameters
{
  public class PaginationResourceParameters
  {
    private int pageNumber;
    public int PageNumber
    {
      get { return pageNumber; }
      set
      {
        if (value >= 1)
        {
          pageNumber = value;
        }
      }
    }

    private int pageSize = 10;
    const int MAX_PAGE_SIZE = 50;
    public int PageSize
    {
      get { return pageSize; }
      set
      {
        if (value >= 1)
        {
          pageSize = (value > MAX_PAGE_SIZE) ? MAX_PAGE_SIZE : value;
        }
      }
    }
  }
}
