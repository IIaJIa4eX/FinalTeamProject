using DatabaseConnector.DTO;

namespace DatabaseConnector;
#pragma warning disable CS8618 // ����, �� ����������� �������� NULL, ������ ��������� ��������, �������� �� NULL, ��� ������ �� ������������. ��������, ����� �������� ���� ��� ����������� �������� NULL.

public class SessionInfo
{
    public int SessionId { get; set; }
    public string SessionToken { get; set; }
    public UserDto Account { get; set; }
}
#pragma warning restore CS8618 // ����, �� ����������� �������� NULL, ������ ��������� ��������, �������� �� NULL, ��� ������ �� ������������. ��������, ����� �������� ���� ��� ����������� �������� NULL.