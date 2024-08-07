import { ActivatedRoute } from "@angular/router";
import { Component, OnInit, Input } from "@angular/core";

@Component({
  selector: "app-common-nav-bar",
  templateUrl: "./common-nav-bar.component.html",
  styleUrls: ["./common-nav-bar.component.css"],
})
export class CommonNavBarComponent implements OnInit {
  @Input() queryPrams;
  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe((querystring) => {
      this.queryPrams = querystring.id == undefined ? "" : querystring.id;
    });
  }
}
