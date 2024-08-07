import { Injectable, Inject, Optional } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { ANUBIS_BASE_URL, Result } from "../anubis-api/Anubis-api";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { ToastrService } from "ngx-toastr";

export interface IUploadResponse {
    path: string;
}

@Injectable({
    providedIn: "root",
})
export class FileUploadService {
    private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined =
        undefined;

    constructor(
        @Inject(HttpClient) http: HttpClient,
        private toastr: ToastrService,
        @Optional() @Inject(ANUBIS_BASE_URL) baseUrl?: string
    ) {
        this.http = http;
        this.baseUrl = baseUrl ? baseUrl : "";
    }

    post(file: File): Observable<string> {
        let url_ = this.baseUrl + "/api/File/upload";
        url_ = url_.replace(/[?&]$/, "");
        const content_ = new FormData();
        if (file !== null && file !== undefined)
            content_.append("file", file, file.name);
        return this.http.post(url_, content_).pipe(
            map((v: IUploadResponse) => {
                return v.path;
            })
        );
    }

    postFile(file: File, companyId: number, type: string): Observable<Result> {
        let url_ = this.baseUrl + "/api/File/uploadReadExcel?";
        const content_ = new FormData();
        if (file !== null && file !== undefined)
            content_.append("file", file, file.name);
        if (type !== undefined)
            url_ += "type=" + encodeURIComponent("" + type) + "&";
        if (companyId !== undefined)
            url_ += "id=" + encodeURIComponent("" + companyId) + "&";
        url_ = url_.replace(/[?&]$/, "");

        return this.http.post(url_, content_).pipe(
            map((res: Result) => {
                return res;
            })
        );
    }

    tartarusAttachments(shipmentId : string, userName: string, file: File) {
        let url_ = `${this.baseUrl}/api/Dashboard/SubmitAttachments`;
        url_ = url_.replace(/[?&]$/, "");
        const content_ = new FormData();        
        content_.append("UserName", userName);
        content_.append("ShipmentId", shipmentId);
        if (file !== null && file !== undefined)
        content_.append("file", file, file.name);

        return this.http.post(url_, content_).pipe(
          map((res: Result) => {
            return res;
          })
        );
      }
}
