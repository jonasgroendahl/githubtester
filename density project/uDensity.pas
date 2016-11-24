unit uDensity;

interface


uses
  Winapi.Windows, Winapi.Messages, System.SysUtils, System.Variants, System.Classes, Vcl.Graphics,
  Vcl.Controls, Vcl.Forms, Vcl.Dialogs, Vcl.StdCtrls, IdHTTP,IdSSLOpenSSL, System.JSON;

  //http://docs.density.io/
type
  TForm1 = class(TForm)
    Button1: TButton;
    Label1: TLabel;
    Button2: TButton;
    Button3: TButton;
    procedure Button1Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure Button3Click(Sender: TObject);

  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form1: TForm1;
  Gtoken : string;

implementation

{$R *.dfm}


procedure TForm1.Button1Click(Sender: TObject);
var
  token, server, result, idStr : string;
  client : TIdHTTP;
  jArr: TJSONArray;
  jObj : TJSONObject;
  jVal : TJSONValue;
  LHandler : TIdSSLIOHandlerSocketOpenSSL;
  letter : char;
  ind, position : Integer;

begin
  client := TIdHTTP.Create();
  LHandler := TIdSSLIOHandlerSocketOpenSSL.Create();
  client.IOHandler := LHandler;
  client.AllowCookies := true;
  // split in 3 due to error >>too long string<<;
  token := 'eyJhbGciOiJFUzM4NCIsInR5cCI6IkpXVCJ9.eyJpYXQiOjE0Nzk4Mjg2NDgsImF1dGgiOnsib3JncyI6W3sicGVybXMiOlsiYXBpLm1hbmFnZV9lbnYiLCJhcGk'+'udmlld19lbnYiXSwiaWQiOiI3NDBqRGJLbnpUNnlBc0JOWXg5Q2tlIiwibmFtZSI6IldleGVyIFZpcnR1YWwifV0sInR5cGUiOiJzYW5kYm94In0sInVzZXIiOnsibGFzdCI6Ijc0MGpEYktuelQ2eUFzQk5ZeDlDa2UiLCJlbWFpbCI6Ijc0MGpEYktuelQ2eUFzQk5ZeDlDa2VAZGVuc2l0eS5pbyIsImZpcnN0IjoiT1'+'JHX1VTRVIifSwiZXhwIjoyNTM0MDIzMDA3OTl9.Xtcq1vAse0bM8Nu9L_4xqlcSgdM7qTb-1eWnkYfdKt4cCjCjoJy8w4rY2q9OefCMvpCCQSMPeu2F5MBAa93Buvs9Bdj5kWXOKvJtu2fkx6KpcGDsRMAwFPBciUir-2au';
  //  Request -> Specifies the header values to send to the HTTP server.
  // http://stackoverflow.com/questions/38272627/how-to-add-a-authorization-bearer-header-with-indy-in-delphi
  client.Request.CustomHeaders.Values['Authorization']:= 'Bearer '+token;
  server := 'https://sandbox.density.io/v1/spaces/';
  try
      result := client.Get(server);
      ShowMessage('List spaces sent succesfully');
      OutputDebugString(PChar('result '+result));
  except
    on E : Exception do
      ShowMessage(E.Message);
      // 400 Bad Request, 403 Forbidden, 404 Not Found, 409 Conflict

  end;
  // http://stackoverflow.com/questions/15947774/getting-json-data-from-a-website-using-delphi/15948106#15948106
  // http://stackoverflow.com/questions/35748913/get-specific-value-from-json-string-delphi-xe8
  // EXTRACT JSON STRING
  jObj := TJSONObject.ParseJSONValue(result) as TJSONObject;
  jArr := jObj.GetValue('results') as TJSONArray;
  OutputDebugString(PChar('count: '+jArr.Count.ToString()));
  // iterate over all the spaces
  for ind := 0 to jArr.Count-1 do
    begin
      jVal := jArr.Get(ind) as TJSONValue;
      idStr := jVal.ToJSON;
      OutputDebugString(PChar('space: '+idStr));
      // Change string "space" to fetch id in a space
      Delete(idStr,1,7);
      position := ansipos('name',idStr);
      Delete(idStr,position-3,idStr.Length);
      OutputDebugString(PChar('id of space: '+idStr ));
    end;

end;

procedure TForm1.Button2Click(Sender: TObject);
var
  token, server, result : string;
  client : TIdHTTP;
  listeS : TStringStream;
  jArr: TJSONArray;
  jObj : TJSONObject;
  jStr : TJSONString;
  jVal : TJSONValue;
  LHandler : TIdSSLIOHandlerSocketOpenSSL;
  letter : char;
begin
  client := TIdHTTP.Create();
  LHandler := TIdSSLIOHandlerSocketOpenSSL.Create();
  listeS := TStringStream.Create('{"name": "Kitchen","description" : "Office Kitchen","reset_times": { "Monday": "08:00:00", "Saturday": "08:00:00"}}');
  client.IOHandler := LHandler;
  client.AllowCookies := true;
  client.Request.CustomHeaders.Values['Authorization']:= 'Bearer '+Gtoken;
  client.Request.ContentType := 'application/json';
  server := 'https://sandbox.density.io/v1/spaces/';
  try
     result :=  client.Post(server,listeS);
  except
    on E : Exception do
      ShowMessage(E.Message);
      // 400 Bad Request, 403 Forbidden, 404 Not Found, 409 Conflict
  end;
  ShowMessage(result);


end;

procedure TForm1.Button3Click(Sender: TObject);
begin
Gtoken :=  'eyJhbGciOiJFUzM4NCIsInR5cCI6IkpXVCJ9.eyJpYXQiOjE0Nzk4Mjg2NDgsImF1dGgiOnsib3JncyI6W3sicGVybXMiOlsiYXBpLm1hbmFnZV9lbnYiLCJhcGk'+'udmlld19lbnYiXSwiaWQiOiI3NDBqRGJLbnpUNnlBc0JOWXg5Q2tlIiwibmFtZSI6IldleGVyIFZpcnR1YWwifV0sInR5cGUiOiJzYW5kYm94In0sInVzZXIiOnsibGFzdCI6Ijc0MGpEYktuelQ2eUFzQk5ZeDlDa2UiLCJlbWFpbCI6Ijc0MGpEYktuelQ2eUFzQk5ZeDlDa2VAZGVuc2l0eS5pbyIsImZpcnN0IjoiT1'+'JHX1VTRVIifSwiZXhwIjoyNTM0MDIzMDA3OTl9.Xtcq1vAse0bM8Nu9L_4xqlcSgdM7qTb-1eWnkYfdKt4cCjCjoJy8w4rY2q9OefCMvpCCQSMPeu2F5MBAa93Buvs9Bdj5kWXOKvJtu2fkx6KpcGDsRMAwFPBciUir-2au';
end;

end.
