namespace EG.LinkedInNet;

public record LinkedInResponse<T>
{
    public PagingType Paging { get; init;  }

    public IList<T> Elements { get; init; }
}

public record PagingType(long total, int start, int count);
