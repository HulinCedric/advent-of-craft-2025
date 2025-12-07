using System.Collections.ObjectModel;
using Day09.Accountability;
using FluentAssertions;
using Xunit;

namespace Day09.Tests;

public class ClientTests
{
    private readonly Client _client;
    private readonly Dictionary<string, double> _orderLines;

    public ClientTests()
    {
        _orderLines = new Dictionary<string, double>
        {
            { "Tenet Deluxe Edition", 45.99 },
            { "Inception", 30.50 },
            { "The Dark Knight", 30.50 },
            { "Interstellar", 23.98 }
        };
        _client = new Client(_orderLines);
    }

    [Fact]
    public void Client_Should_Return_Statement()
    {
        var statement = _client.ToStatement();

        _client.TotalAmount().Should().Be(130.97);
        statement.Should()
            .BeEquivalentTo(
                """
                Tenet Deluxe Edition for 45.99€
                Inception for 30.5€
                The Dark Knight for 30.5€
                Interstellar for 23.98€
                Total : 130.97€
                """);
    }

    [Fact]
    public void TotalAmount_Should_Be_Correct_Without_Calling_ToStatement()
        => _client.TotalAmount().Should().Be(130.97);

    [Fact]
    public void ToStatement_Called_Twice_Should_Not_Double_Total()
    {
        var first = _client.ToStatement();
        var second = _client.ToStatement();

        _client.TotalAmount().Should().Be(130.97);

        first.Should().BeEquivalentTo(second);
    }

    [Fact]
    public void Client_Should_Not_Be_Affected_By_External_Dictionary_Mutations()
    {
        var client = new Client(_orderLines);

        // mutate original dictionary after client creation
        _orderLines["Tenet Deluxe Edition"] = 0.0;
        _orderLines.Remove("Interstellar");

        // client should still report the original total and lines
        client.TotalAmount().Should().Be(130.97);

        var statement = client.ToStatement();
        statement.Should().Contain("Tenet Deluxe Edition for 45.99€");
        statement.Should().Contain("Interstellar for 23.98€");
    }

    [Fact]
    public void Client_With_Empty_OrderLines_Should_Return_Zero_Total()
    {
        var client = new Client(ReadOnlyDictionary<string, double>.Empty);

        client.TotalAmount().Should().Be(0);

        var statement = client.ToStatement();
        statement.Should().Be("Total : 0€");
    }
    
    [Fact]
    public void ToStatement_Should_Not_Expose_FloatingPoint_Artifacts()
    {
        var client = new Client(new Dictionary<string, double>
        {
            { "Small item", 0.1 },
            { "Another small item", 0.2 }
        });

        var statement = client.ToStatement();

        statement.Should()
            .BeEquivalentTo(
                """
                Small item for 0.1€
                Another small item for 0.2€
                Total : 0.3€
                """);
    }
}