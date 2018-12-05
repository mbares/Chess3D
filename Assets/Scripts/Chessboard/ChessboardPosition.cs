using System;

[Serializable]
public class ChessboardPosition
{
    public static string[] columnLabels = { "a", "b", "c", "d", "e", "f", "g", "h" };

    public int column;
    public int row;

    public ChessboardPosition(int column, int row)
    {
        this.column = column;
        this.row = row;
    }

    public static ChessboardPosition operator +(ChessboardPosition a, ChessboardPosition b)
    {
        int columnSum = a.column + b.column;
        int rowSum = a.row + b.row;
        return new ChessboardPosition(columnSum, rowSum);
    }

    public static ChessboardPosition operator *(ChessboardPosition a, int multiplier)
    {
        int columnProduct = a.column * multiplier;
        int rowProduct = a.row * multiplier;
        return new ChessboardPosition(columnProduct, rowProduct);
    }

    public override string ToString()
    {
        return columnLabels[column] + (row + 1);
    }

    public override bool Equals(object obj)
    {
        var position = obj as ChessboardPosition;
        return position != null &&
               column == position.column &&
               row == position.row;
    }

    public override int GetHashCode()
    {
        var hashCode = 815904954;
        hashCode = hashCode * -1521134295 + column.GetHashCode();
        hashCode = hashCode * -1521134295 + row.GetHashCode();
        return hashCode;
    }
}