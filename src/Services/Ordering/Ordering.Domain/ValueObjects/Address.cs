﻿namespace Ordering.Domain.ValueObjects;

public record Address
{
    public string Firstname { get; } = default!;
    public string Lastname { get; } = default!;
    public string? EmailAddress { get; } = default!;
    public string AddressLine { get; } = default!;
    public string Country { get; } = default!;
    public string State { get; } = default!;
    public string ZipCode { get; } = default!;

}
