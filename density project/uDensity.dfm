object Form1: TForm1
  Left = 0
  Top = 0
  Caption = 'Form1'
  ClientHeight = 299
  ClientWidth = 635
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object Label1: TLabel
    Left = 128
    Top = 24
    Width = 360
    Height = 24
    Caption = 'SANDBOX - DENSITY SENSOR PROGRAM'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -20
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
  end
  object Button1: TButton
    Left = 56
    Top = 144
    Width = 121
    Height = 73
    Caption = 'Get spaces'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -16
    Font.Name = 'SimSun'
    Font.Pitch = fpVariable
    Font.Style = [fsBold, fsItalic, fsUnderline]
    Font.Quality = fqNonAntialiased
    ParentFont = False
    TabOrder = 0
    OnClick = Button1Click
  end
  object Button2: TButton
    Left = 208
    Top = 153
    Width = 105
    Height = 57
    Caption = 'Create space'
    TabOrder = 1
    OnClick = Button2Click
  end
  object Button3: TButton
    Left = 56
    Top = 64
    Width = 75
    Height = 25
    Caption = 'main'
    TabOrder = 2
    OnClick = Button3Click
  end
end
