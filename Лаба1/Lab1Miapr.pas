unit Lab1Miapr;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, ExtCtrls, StdCtrls;

type
  TClassImage = record
      X: Integer;
      Y: Integer;
      IsCenter: Boolean;
      ClassNum: Integer;
  end;
  TForm1 = class(TForm)
    ClassesAmountEdit: TEdit;
    ImagesAmountEdit: TEdit;
    InitButton: TButton;
    Button2: TButton;
    Button3: TButton;
    Label1: TLabel;
    Label2: TLabel;
    Image1: TImage;
    Button1: TButton;
    Button4: TButton;
    Button5: TButton;
    procedure InitButtonCliick(Sender: TObject);
    procedure ClassesAmountEditKeyPress(Sender: TObject; var Key: Char);
    procedure ImagesAmountEditKeyPress(Sender: TObject; var Key: Char);
    procedure Button2Click(Sender: TObject);
    procedure ImagesAmountEditChange(Sender: TObject);
    procedure Button3Click(Sender: TObject);
    procedure Button4Click(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure Button5Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

Type Arr = Array  Of TClassImage;

const
    IMAGE_RADIUS = 5;

var
  Form1: TForm1;
  Images: Arr;
  ClassesAmount: Byte;
  ImagesAmount: Integer;
  MinMaxClassCenters: Arr;
  ClassCenters: Arr;
  NextCenter: TClassImage;


implementation

{$R *.dfm}

function FindClassCenter(const Points: array of TClassImage; oldCenter: TClassImage): TClassImage;
var
  i, j: Integer;
  MinDistanceSum: Double;
  DistanceSum: Double;
  CenterPoint: TClassImage;
begin
  MinDistanceSum := MaxInt;
  CenterPoint := oldCenter;
  for i := 0 to High(Points) do
  begin
    DistanceSum := 0;
    for j := 0 to High(Points) do
    begin
      if i <> j then
      begin
        DistanceSum := DistanceSum + Sqrt(Sqr(Points[i].X - Points[j].X) + Sqr(Points[i].Y - Points[j].Y));
      end;
    end;
    if DistanceSum < MinDistanceSum then
    begin
      MinDistanceSum := DistanceSum;
      CenterPoint := Points[i];
    end;
  end;
  Result := CenterPoint;
end;

procedure UpdateClassCenters;
var
  i, j: Integer;
  ClassNum: byte;
  ClassImages: array of TClassImage;
  SumX, SumY: Integer;
  Count: Integer;
  NewCenter: TClassImage;
  UpdatedClasses: Set Of byte;
begin
Count := 0;
  UpdatedClasses := [];
  for i := 0 to High(Images) do
  begin
        if Images[i].IsCenter and not (Images[I].ClassNum in UpdatedClasses) then
        begin
          inc(Count);
          Include(UpdatedClasses, Images[I].ClassNum);

          Images[i].IsCenter := False;
          ClassNum := Images[i].ClassNum;
         // Images[I].ClassNum := -1;
          SetLength(ClassImages, 0);

          for j := 0 to High(Images) do
          begin
            if Images[j].ClassNum = ClassNum then
            begin
              SetLength(ClassImages, Length(ClassImages) + 1);
              ClassImages[High(ClassImages)] := Images[j];
           end;
          end;

          NewCenter := FindClassCenter (ClassImages, Images[i]);
          for j := 0 to High(Images) do
          Begin
              If (NewCenter.X = Images[j].X) And (NewCenter.Y = Images[j].Y) Then
              Begin
                  Images[j].IsCenter := True;
                  Break;
              End;
          end;
       end;
  end;
end;

procedure ClassifyImages;
var
  i, j: Integer;
  MinDistance, Distance: Double;
begin
  SetLength(ClassCenters, 0);
  for i := 0 to High(Images) do
  begin
    if Images[i].IsCenter then
    begin
      SetLength(ClassCenters, Length(ClassCenters) + 1);
      ClassCenters[High(ClassCenters)] := Images[i];
      if length(ClassCenters) = ClassesAmount then
          Break;
    end;
  end;
    i := 228;
    for i := 0 to High(Images) do
    BEGIN
        if Not (Images[i].IsCenter) Then
        begin
             MinDistance := MaxInt;
             for j := 0 to High(ClassCenters) do
             begin
                Distance := (Sqr(Images[i].X - ClassCenters[j].X) + Sqr(Images[i].Y - ClassCenters[j].Y));
                if Distance < MinDistance then
                begin
                    MinDistance := Distance;
                    Images[i].ClassNum := ClassCenters[j].ClassNum;
                end;
             END;
        end;
    end;
end;


procedure DrawUnclassifiedImages();
Var I, count: Integer;
Begin
    count := 0;
    With Form1 Do
    Begin
        Image1.Canvas.Brush.Color := clYellow;
        For I := 0 To ImagesAmount - 1 Do
        Begin
            Image1.Canvas.Ellipse(Images[I].X, Images[I].Y, Images[I].X + IMAGE_RADIUS, Images[I].Y + IMAGE_RADIUS);
        End;

        For I := 0 To ImagesAmount - 1 Do
        If Images[I].IsCenter Then
        Begin
            Image1.Canvas.Brush.Color := clRed;
            Image1.Canvas.Ellipse(Images[I].X, Images[I].Y, Images[I].X + IMAGE_RADIUS*2, Images[I].Y + IMAGE_RADIUS*2);
            Image1.Canvas.Brush.Color := clYellow;
        End
        Else
            Break;
        Image1.Canvas.Brush.Color := clBlack;
    end;
ENd;

procedure DrawClassifiedImages();
Var I, j: Integer;
Begin
    With Form1 Do
    Begin
        Image1.Canvas.Brush.Color := clBlack;
        Image1.Canvas.FillRect(Rect(0, 0, Image1.Width, Image1.Height));
        For I := 0 To High(Images) Do
        Begin
            case Images[I].ClassNum Mod ClassesAmount of
            0: Image1.Canvas.Brush.Color := clNavy;
            1: Image1.Canvas.Brush.Color := clRed;      // Красный
            2: Image1.Canvas.Brush.Color := clGreen;    // Зеленый
            3: Image1.Canvas.Brush.Color := clBlue;     // Синий
            4: Image1.Canvas.Brush.Color := clYellow;   // Желтый
            5: Image1.Canvas.Brush.Color := clPurple;   // Фиолетовый
            6: Image1.Canvas.Brush.Color := clAqua;     // Бирюзовый
            7: Image1.Canvas.Brush.Color := clLime;     // Лаймовый
            8: Image1.Canvas.Brush.Color := clFuchsia;  // Малиновый
            9: Image1.Canvas.Brush.Color := clGray;     // Серый
            10: Image1.Canvas.Brush.Color := 222222;   // Белый
            11: Image1.Canvas.Brush.Color := clMaroon;
            12: Image1.Canvas.Brush.Color := clSilver;
            13: Image1.Canvas.Brush.Color := clCream;
            14: Image1.Canvas.Brush.Color := clSkyBlue;
            15: Image1.Canvas.Brush.Color := 99999;
            16: Image1.Canvas.Brush.Color := 77777;
            17: Image1.Canvas.Brush.Color := 44444;
            18: Image1.Canvas.Brush.Color := 22200;
            19: Image1.Canvas.Brush.Color := 55550;
            end;
                Image1.Canvas.Ellipse(Images[I].X, Images[I].Y, Images[I].X + IMAGE_RADIUS, Images[I].Y + IMAGE_RADIUS);

        end;
        Image1.Canvas.Brush.Color := clRed;
        For I := 0 To High(Images) Do
        Begin
         If Images[I].IsCenter Then
            Begin
                Image1.Canvas.Ellipse(Images[I].X, Images[I].Y, Images[I].X + IMAGE_RADIUS*3, Images[I].Y + IMAGE_RADIUS*3);
           End
        End;
    end;
ENd;


procedure InitImages();
var i, rand: integer;
    CentersCount: integer;
Begin
    CentersCount := 1;
    rand := GetTickCount();
    for I := 0 To rand Do
        Rand := Random(rand);
    For I := 0 To High(Images) Do
    Begin
        rand := 700 - rand;
        Images[i].X := Random(700) + 10;
        Images[i].Y := Random(700) + 10;
        Images[i].ClassNum := -1;
        if (CentersCount <= ClassesAmount) Then
        Begin
           Images[i].IsCenter := true;
           Images[i].ClassNum := I + 1;
           Inc(CentersCount);
        end
        else
            Images[i].IsCenter := false;
    End;
End;

procedure TForm1.InitButtonCliick(Sender: TObject);
begin
    if ImagesAmountEdit.Text = '' then
     exit;
     if ClassesAmountEdit.Text = '' then
     exit;
    ClassesAmount := StrToInt(ClassesAmountEdit.text);
    ImagesAmount := strToInt(ImagesAmountEdit.Text);
    SetLength(Images, ImagesAmount);
    InitImages();
    Image1.Canvas.Brush.Color := clBlack;
    Image1.Canvas.FillRect(Rect(0, 0, Image1.Width, Image1.Height));
    if (length(Images) > 0) Then
        DrawUnclassifiedImages();
end;

procedure TForm1.ClassesAmountEditKeyPress(Sender: TObject; var Key: Char);
begin
    if ((key < '0') or (Key > '9')) then
        if ( key <> #8)  then
          key := #0;
end;

procedure TForm1.ImagesAmountEditKeyPress(Sender: TObject; var Key: Char);
begin
    if ((key < '0') or (Key > '9')) then
        if ( key <> #8) then
        key := #0;
end;

procedure TForm1.Button2Click(Sender: TObject);
begin
    ClassifyImages;
    UpdateClassCenters;
    Image1.Canvas.FillRect(Rect(0, 0, Image1.Width, Image1.Height));
    DrawClassifiedImages();
end;

procedure TForm1.ImagesAmountEditChange(Sender: TObject);
var amount: Integer;
begin
    amount := 0;
    try
        if Not (ImagesAmountEdit.Text = '') then
            amount := StrToInt(ImagesAmountEdit.Text);
        if (amount > 100000) then
        Begin
            Showmessage('Too many Images!');
            ImagesAmountEdit.Text := '99999';
        End;
   except

    end;
end;

FUNCTION ArrsEqual(Arr1: Arr; Arr2: Arr): Boolean;
var i: Integer;
Begin
    result := true;
    If (Length(arr1) <> Length(Arr2)) Then
    Begin
        result :=  false;
        exit;
    End;
    For i := 0 To High(Arr1) DO
    Begin
        if ((arr1[i].x <> arr2[i].x ) Or (arr1[i].Y <> arr2[i].Y)) then
        Begin
            result :=  false;
        exit;
        End;
    end;
End;

procedure TForm1.Button3Click(Sender: TObject);
var temp: Arr;
begin
    SetLength(Temp, 1);
    while (Not  ArrsEqual(Temp, ClassCenters)) DO
    Begin
        Temp := ClassCenters;
        ClassifyImages;
        UpdateClassCenters;
    End;

    DrawclassifiedImages();

end;

////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////

procedure InitMaximinImages();
var i: integer;
    rand: integer;
Begin
    rand := GetTickCount();
    for I := 0 To rand Do
        Rand := Random(rand);
    For I := 0 To High(Images) Do
    Begin
        Images[i].X := Random(700) + 10;
        Images[i].Y := Random(700) + 10;
        Images[i].ClassNum := 1;
        Images[i].IsCenter := false;
    End;
    Images[0].IsCenter := True;
    SetLength(MinMaxClassCenters, 1);
    MinMaxClassCenters[0] := Images[0];
End;

function AvgDistanceBetweenCenters(): Integer;
var
  TotalDistance: Integer;
  Count: Integer;
  i, j: Integer;
begin
  TotalDistance := 0;
  Count := 0;
  for i := Low(MinMaxClassCenters) to High(MinMaxClassCenters) do
  begin
    for j := i to High(MinMaxClassCenters) do
    begin
      TotalDistance := TotalDistance + Trunc(Sqrt(Sqr(MinMaxClassCenters[i].X - MinMaxClassCenters[j].X) + Sqr(MinMaxClassCenters[i].Y - MinMaxClassCenters[j].Y)));
      Count := Count + 1;
    end;
  end;

  if Count = 0 then
    Result := 0
  else
    Result := TotalDistance div Count;
end;

function FindNextCenter(): boolean;
Var  I, J, index, MaxDistIndex: Integer;
     Distance, MaxDistance, MaxDistAmongClasses: Integer;
                                d: integer;
Begin
    result := true;
    MaxDistAmongClasses := 0;
    MaxDistIndex := 0;
    for j := 0 to High(MinMaxClassCenters) do
    begin
        MaxDistance := 0;
        for I := 0 to High(Images) do
        If Not Images[i].IsCenter And (Images[i].ClassNum = MinMaxClassCenters[j].ClassNum) then
        Begin
            Distance := Trunc(Sqrt(Sqr(Images[i].X - MinMaxClassCenters[j].X) + Sqr(Images[i].Y - MinMaxClassCenters[j].Y)));
            if Distance > MaxDistance then
            begin
                MaxDistance := Distance;
                index := i;
            end;
       End;
       If MaxDistance > MaxDistAmongClasses Then
       Begin
           MaxDistIndex := Index;
           MaxDistAmongClasses := MaxDistance;
       End;
       d :=  AvgDistanceBetweenCenters()  div 3;
       If Distance < d Then
          result := false;
    end;
    SetLength(MinMaxClassCenters, Length(MinMaxClassCenters) + 1);
    MinMaxClassCenters[High(MinMaxClassCenters)] :=  Images[index];
    Images[index].IsCenter := True;
    Images[index].ClassNum := Length(MinMaxClassCenters);
    ClassesAmount := Length(MinMaxClassCenters) + 1;
End;



procedure TForm1.Button4Click(Sender: TObject);
Begin
if ImagesAmountEdit.Text = '' then
     exit;
    ImagesAmount := strToInt(ImagesAmountEdit.Text);
    SetLength(ClassCenters, 1);
    SetLength(Images, ImagesAmount);
    InitMaximinImages();
    Image1.Canvas.Brush.Color := clBlack;
    Image1.Canvas.FillRect(Rect(0, 0, Image1.Width, Image1.Height));
    if (length(Images) > 0) Then
        DrawUnclassifiedImages();
end;

procedure TForm1.Button1Click(Sender: TObject);
Var I: Integer;
    Distance, MaxDistance: Integer;
    SecondCenterIndex : Integer;
begin
     if ImagesAmountEdit.Text = '' then
     exit;
    If Length(ClassCenters) >= 2 Then
    Begin
        FindNextCenter();
    End
    Else
    Begin
        MaxDistance := 0;
        SetLength(MinMaxClassCenters, 2);
        For I := 0 To High(Images) DO
        Begin
            Distance :=  Sqr(Images[i].X - MinMaxClassCenters[0].X) + Sqr(Images[i].Y - MinMaxClassCenters[0].Y);
            if Distance > MaxDistance Then
            Begin
                SecondCenterIndex := i;
                MaxDistance := Distance;
                MinMaxClassCenters[1] := Images[i];
            End;
        End;
        ClassesAmount := 2;
        Images[SecondCenterIndex].IsCenter := true;
        Images[SecondCenterIndex].ClassNum:= 2;
    End;
    ClassesAmount := Length(MinMaxClassCenters);
    ClassifyImages();
    DrawClassifiedImages();
end;

procedure TForm1.FormCreate(Sender: TObject);
begin
    SetLength(MinMaxClassCenters, 1);
    ClassesAmount := 1;
end;

procedure TForm1.Button5Click(Sender: TObject);
Var I: Integer;
    Distance, MaxDistance: Integer;
    SecondCenterIndex : Integer;
    Count: Integer;
begin
    count := 0;
while(true) do
Begin
    inc(count);
    If Length(ClassCenters) >= 2 Then
    Begin
        if Not FindNextCenter() Then
        Begin
            ClassifyImages();
            DrawClassifiedImages();
              exit;
        end;
    End
    Else
    Begin
        MaxDistance := 0;
        SetLength(MinMaxClassCenters, 2);
        For I := 0 To High(Images) DO
        Begin
            Distance :=  Sqr(Images[i].X - MinMaxClassCenters[0].X) + Sqr(Images[i].Y - MinMaxClassCenters[0].Y);
            if Distance > MaxDistance Then
            Begin
                SecondCenterIndex := i;
                MaxDistance := Distance;
                MinMaxClassCenters[1] := Images[i];
            End;
        End;
        ClassesAmount := 2;
        Images[SecondCenterIndex].IsCenter := true;
        Images[SecondCenterIndex].ClassNum:= 2;
    End;
    ClassesAmount := Length(MinMaxClassCenters);
    ClassifyImages();
end;
    showmessage(IntToStr(Count));
 //  DrawClassifiedImages();
end;

end.
