module FSharp.Bolero.Client.Main

open Elmish
open Bolero
open Bolero.Remoting.Client
open Bolero.Templating.Client
open Microsoft.Extensions.Configuration

type Page =
    | [<EndPoint "/">] Home

type Model =
    { Page : Page
      SomeSetting : string }

let init (c : IConfiguration) _ =
    { Page = Home
      SomeSetting = c.["SomeSetting"] }
    , Cmd.none

type Message =
    | SetPage of Page

let update message model =
    match message with
    | SetPage p -> { model with Page = p }, Cmd.none

let router = Router.infer SetPage (fun model -> model.Page)

type Main = Template<"wwwroot/main.html">

let view model dispatch =
    Main().SomeSetting(model.SomeSetting).Elt()

type MyApp() =
    inherit ProgramComponent<Model, Message>()

    override this.Program =
        let configuration = this.Services.GetService(typeof<IConfiguration>) :?> IConfiguration
        
        Program.mkProgram (init configuration) update view
        |> Program.withRouter router