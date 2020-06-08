Imports System.Runtime.InteropServices

Public Class MainFrame

    'Full disclosure: not my code, comes from someone online
    'https://stackoverflow.com/questions/8543765/bring-vb-net-window-on-top-of-all-windows
    Private Const SW_SHOWNOACTIVATE As Integer = 4
    Private Const HWND_TOPMOST As Integer = -1
    Private Const SWP_NOACTIVATE As UInteger = &H10
    Private Const SW_SHOW As Integer = 5
    Private Const SWP_SHOWWINDOW As UInteger = &H40

    <DllImport("user32.dll", EntryPoint:="SetWindowPos")>
    Private Shared Function SetWindowPos(ByVal hWnd As Integer, ByVal hWndInsertAfter As Integer, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal uFlags As UInteger) As Boolean
    End Function
    <DllImport("user32.dll")>
    Private Shared Function ShowWindow(ByVal hWnd As IntPtr, ByVal nCmdShow As Integer) As Boolean
    End Function

    Private Shared Sub ShowTopMost(ByVal frm As Form)
        ShowWindow(frm.Handle, SW_SHOW)
        SetWindowPos(frm.Handle.ToInt32(), HWND_TOPMOST, frm.Left, frm.Top, frm.Width, frm.Height, SWP_SHOWWINDOW)
    End Sub

    'Variables and stuff
    Dim userOpacity As Double = 0.9
    Dim map As New Dictionary(Of String, String)
    Dim WithEvents kbh As New KeyboardHook
    Dim qterm As String
    Dim x As Integer
    Dim y As Integer
    Dim firstopen As Boolean = False
    Dim prev As New List(Of String)
    Dim prev_ind As Integer
    Dim ck As Boolean = False
    Dim ctrl As Boolean = False
    Dim rctrl As Boolean = False

    'Background keypress handlers
    Private Sub kbh_KeyDown(Key As Keys) Handles kbh.KeyDown

        If holdTimer.Enabled Then
            Return
        End If

        If Key = Keys.C Then
            ck = True
        End If
        If Key = Keys.LControlKey Then
            ctrl = True
        End If
        If Key = Keys.RControlKey Then
            rctrl = True
        End If
        If ck And ctrl And Not Visible Then
            holdTimer.Enabled = True
        End If

    End Sub

    Private Sub kbh_KeyUp(Key As Keys) Handles kbh.KeyUp

        If Key = Keys.C Then
            holdTimer.Enabled = False
            ck = False
        End If

        If Key = Keys.LControlKey Then
            holdTimer.Enabled = False
            ctrl = False
        End If

        If Key = Keys.RControlKey Then
            rctrl = False
        End If

    End Sub

    'Misc animation stuff
    Private Sub animateShow_Tick(sender As Object, e As EventArgs) Handles animateShow.Tick
        If Visible = False Then
            animateHide.Enabled = False
            If Not Clipboard.ContainsText Then
                Return
            End If
            Opacity = 0
            firstopen = True
            ShowTopMost(Me)
            auxBrowser.Hide()
            prev.Clear()
            prev_ind = -1
            auxBrowser.Navigate("https://en.wiktionary.org/wiki/" + Clipboard.GetText().Replace("ā", "a").Replace("ē", "e").Replace("ī", "i").Replace("ō", "o").Replace("ū", "u"))
        End If
        If Opacity < userOpacity Then
            Opacity = Math.Min(Opacity + 0.1, userOpacity)
        Else
            Opacity = Math.Min(Opacity, userOpacity)
            animateShow.Enabled = False
        End If
    End Sub

    Private Sub animateHide_Tick(sender As Object, e As EventArgs) Handles animateHide.Tick
        If Opacity <= userOpacity Then
            animateShow.Enabled = False
        End If
        If Opacity > 0 Then
            Opacity = Math.Max(Opacity - 0.1, 0)
        Else
            Opacity = Math.Max(Opacity, 0)
            animateHide.Enabled = False
            Hide()
        End If
    End Sub

    Private Sub holdTimer_Tick(sender As Object, e As EventArgs) Handles holdTimer.Tick
        animateShow.Enabled = True
    End Sub

    'Actual processing starts here
    Dim parts As New List(Of String)({"Etymology", "Adjective", "Adverb", "Ambiposition", "Article", "Circumposition", "Classifier", "Conjunction", "Contraction", "Counter", "Determiner", "Ideophone", "Interjection", "Noun", "Numeral", "Participle", "Particle", "Postposition", "Preposition", "Pronoun", "Proper noun", "Verb"})

    Private Sub auxBrowser_DocumentCompleted(sender As Object, e As WebBrowserDocumentCompletedEventArgs) Handles auxBrowser.DocumentCompleted

        Dim lStart As HtmlElement

        'Look for the start of the Latin section
        Try

            If auxBrowser.Document.GetElementById("Latin") = Nothing OrElse auxBrowser.Document.GetElementById("Latin").Parent = Nothing OrElse auxBrowser.Document.GetElementById("Latin").Parent.NextSibling = Nothing Then

                If Not qterm.ToLower = qterm Then
                    prev.RemoveAt(prev_ind)
                    prev_ind -= 1
                    auxBrowser.Navigate("https://en.wiktionary.org/wiki/" + qterm.Replace("ā", "a").Replace("ē", "e").Replace("ī", "i").Replace("ō", "o").Replace("ū", "u").ToLower)
                    Return
                End If
                display.Text = "Not a Latin word. | " & qterm
                displayMem = "Not a Latin word. | " & qterm
                ghost.Select()
                Return

            End If

            lStart = auxBrowser.Document.GetElementById("Latin").Parent.NextSibling
        Catch
            If Not qterm.ToLower = qterm Then
                prev.RemoveAt(prev_ind)
                prev_ind -= 1
                auxBrowser.Navigate("https://en.wiktionary.org/wiki/" + qterm.Replace("ā", "a").Replace("ē", "e").Replace("ī", "i").Replace("ō", "o").Replace("ū", "u").ToLower)
                Return
            End If
            display.Text = "Not a Latin word. | " & qterm
            displayMem = "Not a Latin word. | " & qterm
            ghost.Select()
            Return
        End Try

        Dim str As String = ""
        Dim senses As Integer = 0

        'More fancy animations
        Width = 400
        Height = 200
        rsz()

        'Look over the HTML until you either find the start of the next section (or the end of the page)
        Try

            Do Until (lStart.TagName = "H2")

                If lStart.FirstChild = Nothing Then

                    lStart = lStart.NextSibling

                    If lStart = Nothing Then
                        Exit Do
                    End If

                    Continue Do
                End If

                Try

                    'If you see the start of a definition (which is like 'Verb' or 'Participle' on the webpage), or Etymology, add it to the list of things to display (which is just a string because HTML)
                    If lStart.FirstChild.TagName = "SPAN" And parts.Contains(lStart.FirstChild.InnerText) Then

                        If lStart.FirstChild.InnerText = "Etymology" Then

                            str = str & lStart.OuterHtml & vbNewLine & lStart.NextSibling.OuterHtml & vbNewLine

                        Else

                            str = str & lStart.OuterHtml & vbNewLine & lStart.NextSibling.OuterHtml & vbNewLine & lStart.NextSibling.NextSibling.OuterHtml & vbNewLine

                            Try
                                If lStart.NextSibling.NextSibling.NextSibling.FirstChild.InnerText = "Declension" Then
                                    str = str & lStart.NextSibling.NextSibling.NextSibling.OuterHtml & vbNewLine & lStart.NextSibling.NextSibling.NextSibling.NextSibling.OuterHtml & vbNewLine
                                End If
                            Catch
                            End Try

                            senses += 1

                        End If

                    End If

                    'Add any tables seen
                    Try
                        If lStart.TagName = "TABLE" Then

                            'Make the window bigger to show the table
                            Width = 800
                            Height = 400
                            rsz()

                            str = str & lStart.OuterHtml & vbNewLine

                            'Replace all the colors in the table with darker colors for readability
                            For Each c In map
                                str = str.Replace(c.Key, c.Value)
                            Next

                        End If
                    Catch
                    End Try

                Catch
                End Try

                'Go to next element
                lStart = lStart.NextSibling

                'Check for end of page
                If lStart = Nothing Then
                    Exit Do
                End If

            Loop

        Catch
            display.Text = "An error occurred. | " & qterm
            displayMem = "An error occurred. | " & qterm
            Return
        End Try

        'Fancy regex stuff, removes all the [edit]s on the page
        str = System.Text.RegularExpressions.Regex.Replace(str, "<SPAN class=mw-editsection>.+?</SPAN></SPAN>", "")

        '[hacky!] Make the webpage (basically chuck a bunch of CSS in the head and then dump the string we've been building up in the body, HTML does the rest for you)
        auxBrowser.Document.Write("
<!DOCTYPE html>
<html>
    <head>
        <style>
            body { background-color: #282828; font-family: sans-serif  }
            span, a, i, th, td, p, h3, h2, ol { color: #ffffff; font-size: 9pt; line-height: 1 }
            th { background-color: #38C4B6 }
            a { text-decoration: none }
        </style>
    </head>
    <body>
        " & str & "
    </body>
</html>")

        'Darken the border of the table (readability)
        Try
            auxBrowser.Document.GetElementsByTagName("TABLE")(0).Style =
                "FONT-SIZE: 95%; BORDER-TOP: #282828 1px solid; BORDER-RIGHT: #282828 1px solid; WIDTH: 100%; BACKGROUND: #282828; BORDER-BOTTOM: #282828 1px solid; TEXT-ALIGN: center; BORDER-LEFT: #282828 1px solid"

        Catch
        End Try

        If senses = 1 Then
            display.Text = "1 sense | " + qterm
            displayMem = "1 sense | " + qterm
        Else
            display.Text = senses.ToString() + " senses | " + qterm
            displayMem = senses.ToString() + " senses | " + qterm
        End If

        'The browser's been hidden this whole time to save your eyes
        auxBrowser.Show()
        ghost.Select()

    End Sub

    Private Sub MainFrame_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Load up the list of colors we want to switch to a different color
        map.Add("#c0cfe4", "#5479AD")
        map.Add("#ccc", "#656565")
        map.Add("#c0e4c0", "#489148")
        map.Add("#e4d4c0", "#9E7B53")
        map.Add("#e0e0b0", "#9E9E3C")
        map.Add("#e2e4c0", "#B5B529")
        map.Add("#f8f8ff", "#404040")
        map.Add("#40e0d0", "#38C4B6")
    End Sub

    Private Sub interLoad_Tick(sender As Object, e As EventArgs) Handles interLoad.Tick
        'Hide on start (for some reason Form.Load is unreliable)
        interLoad.Enabled = False
        Hide()
    End Sub

    Private Sub auxBrowser_Navigating(sender As Object, e As WebBrowserNavigatingEventArgs) Handles auxBrowser.Navigating

        'UI stuff
        qterm = e.Url.OriginalString.Replace("https://en.wiktionary.org/wiki/", "").Replace("#Latin", "")
        display.Text = "Querying " & qterm & "..."
        displayMem = "Querying " & qterm & "..."
        ghost.Select()

        'Maintain a history for the back/forward buttons
        If (prev_ind = prev.Count - 1) And (prev_ind = -1 OrElse Not prev(prev_ind) = qterm) Then
            prev.Add(qterm)
            prev_ind += 1
        End If

        'UI stuff
        Height = 25
        Width = 400
        If firstopen Then
            x = MousePosition.X
            y = MousePosition.Y
            firstopen = False
        End If
        rsz()

        'If clicking a link on the wiki, sometimes it gets clipped, so this just makes sure it's a proper link
        If e.Url.OriginalString.StartsWith("about:") Then

            e.Cancel = True
            prev.RemoveAt(prev_ind)
            prev_ind -= 1
            auxBrowser.Navigate(e.Url.OriginalString.Replace("about:", "https://en.wiktionary.org"))

        End If

    End Sub

    'Normal VB UI stuff
    Sub rsz()
        auxBrowser.Top = 25
        auxBrowser.Left = 0
        auxBrowser.Height = Height - 25
        auxBrowser.Width = Width + 20
        display.Width = Width - 60
        If x + Width < Screen.PrimaryScreen.WorkingArea.Width Then
            Left = x
        Else
            Left = Screen.PrimaryScreen.WorkingArea.Width - Width
        End If
        If y + Height < Screen.PrimaryScreen.WorkingArea.Height Then
            Top = y
        Else
            Top = Screen.PrimaryScreen.WorkingArea.Height - Height
        End If
    End Sub

    Dim displayMem As String = ""

    Private Sub display_Enter(sender As Object, e As EventArgs) Handles display.Enter
        displayMem = display.Text
        display.Text = ""
    End Sub

    Private Sub display_Leave(sender As Object, e As EventArgs) Handles display.Leave
        display.Text = displayMem
    End Sub

    Private Sub display_KeyDown(sender As Object, e As KeyEventArgs) Handles display.KeyDown
        If e.KeyCode = Keys.Return Then
            prev.Clear()
            prev_ind = -1
            auxBrowser.Hide()
            auxBrowser.Navigate("https://en.wiktionary.org/wiki/" & display.Text.Replace("ā", "a").Replace("ē", "e").Replace("ī", "i").Replace("ō", "o").Replace("ū", "u"))
        End If
        If e.KeyCode = Keys.Escape Then
            ghost.Select()
        End If
    End Sub

    Private Sub b_forward_Click(sender As Object, e As EventArgs) Handles b_forward.Click
        If prev_ind >= prev.Count - 1 Then
            Return
        End If
        prev_ind += 1
        auxBrowser.Navigate("https://en.wiktionary.org/wiki/" & prev(prev_ind))
    End Sub

    Private Sub buttonCheck_Tick(sender As Object, e As EventArgs) Handles buttonCheck.Tick
        If prev_ind > 0 Then
            b_back.Enabled = True
        Else
            b_back.Enabled = False
        End If
        If prev_ind < prev.Count - 1 Then
            b_forward.Enabled = True
        Else
            b_forward.Enabled = False
        End If
    End Sub

    Private Sub b_back_Click(sender As Object, e As EventArgs) Handles b_back.Click
        If prev_ind <= 0 Then
            Return
        End If
        prev_ind -= 1
        auxBrowser.Navigate("https://en.wiktionary.org/wiki/" & prev(prev_ind))
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        nicon.Dispose()
        Application.Exit()
    End Sub

    Private Sub moveTimer_Tick(sender As Object, e As EventArgs) Handles moveTimer.Tick

        If MousePosition.X + Width < Screen.PrimaryScreen.WorkingArea.Width Then
            Left = MousePosition.X
        Else
            Left = Screen.PrimaryScreen.WorkingArea.Width - Width
        End If
        If MousePosition.Y + Height < Screen.PrimaryScreen.WorkingArea.Height Then
            Top = MousePosition.Y
        Else
            Top = Screen.PrimaryScreen.WorkingArea.Height - Height
        End If

        x = Left
        y = Top
    End Sub

    Private Sub MainFrame_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.M And e.Modifiers = Keys.Control Then
            moveTimer.Enabled = Not moveTimer.Enabled
        End If
        If e.KeyCode = Keys.Escape Then
            moveTimer.Enabled = False
            animateHide.Enabled = True
        End If
    End Sub

    Private Sub auxBrowser_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles auxBrowser.PreviewKeyDown
        If e.KeyCode = Keys.Escape Then
            moveTimer.Enabled = False
            animateHide.Enabled = True
        End If
    End Sub
End Class

'Also not my code, this is a very useful class that I pulled off the link below which gets keypresses without focus
'https://sim0n.wordpress.com/2009/03/28/vbnet-keyboard-hook-class/
Public Class KeyboardHook

    <DllImport("User32.dll", CharSet:=CharSet.Auto, CallingConvention:=CallingConvention.StdCall)>
    Private Overloads Shared Function SetWindowsHookEx(ByVal idHook As Integer, ByVal HookProc As KBDLLHookProc, ByVal hInstance As IntPtr, ByVal wParam As Integer) As Integer
    End Function
    <DllImport("User32.dll", CharSet:=CharSet.Auto, CallingConvention:=CallingConvention.StdCall)>
    Private Overloads Shared Function CallNextHookEx(ByVal idHook As Integer, ByVal nCode As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As Integer
    End Function
    <DllImport("User32.dll", CharSet:=CharSet.Auto, CallingConvention:=CallingConvention.StdCall)>
    Private Overloads Shared Function UnhookWindowsHookEx(ByVal idHook As Integer) As Boolean
    End Function

    <StructLayout(LayoutKind.Sequential)>
    Private Structure KBDLLHOOKSTRUCT
        Public vkCode As UInt32
        Public scanCode As UInt32
        Public flags As KBDLLHOOKSTRUCTFlags
        Public time As UInt32
        Public dwExtraInfo As UIntPtr
    End Structure

    <Flags()>
    Private Enum KBDLLHOOKSTRUCTFlags As UInt32
        LLKHF_EXTENDED = &H1
        LLKHF_INJECTED = &H10
        LLKHF_ALTDOWN = &H20
        LLKHF_UP = &H80
    End Enum

    Public Shared Event KeyDown(ByVal Key As Keys)
    Public Shared Event KeyUp(ByVal Key As Keys)

    Private Const WH_KEYBOARD_LL As Integer = 13
    Private Const HC_ACTION As Integer = 0
    Private Const WM_KEYDOWN = &H100
    Private Const WM_KEYUP = &H101
    Private Const WM_SYSKEYDOWN = &H104
    Private Const WM_SYSKEYUP = &H105

    Private Delegate Function KBDLLHookProc(ByVal nCode As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As Integer

    Private KBDLLHookProcDelegate As KBDLLHookProc = New KBDLLHookProc(AddressOf KeyboardProc)
    Private HHookID As IntPtr = IntPtr.Zero

    Private Function KeyboardProc(ByVal nCode As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As Integer
        If (nCode = HC_ACTION) Then
            Dim struct As KBDLLHOOKSTRUCT
            Select Case wParam
                Case WM_KEYDOWN, WM_SYSKEYDOWN
                    RaiseEvent KeyDown(CType(CType(Marshal.PtrToStructure(lParam, struct.GetType()), KBDLLHOOKSTRUCT).vkCode, Keys))
                Case WM_KEYUP, WM_SYSKEYUP
                    RaiseEvent KeyUp(CType(CType(Marshal.PtrToStructure(lParam, struct.GetType()), KBDLLHOOKSTRUCT).vkCode, Keys))
            End Select
        End If
        Return CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam)
    End Function

    Public Sub New()
        HHookID = SetWindowsHookEx(WH_KEYBOARD_LL, KBDLLHookProcDelegate, System.Runtime.InteropServices.Marshal.GetHINSTANCE(System.Reflection.Assembly.GetExecutingAssembly.GetModules()(0)).ToInt32, 0)
        If HHookID = IntPtr.Zero Then
            Throw New Exception("Could not set keyboard hook")
        End If
    End Sub

    Protected Overrides Sub Finalize()
        If Not HHookID = IntPtr.Zero Then
            UnhookWindowsHookEx(HHookID)
        End If
        MyBase.Finalize()
    End Sub

End Class