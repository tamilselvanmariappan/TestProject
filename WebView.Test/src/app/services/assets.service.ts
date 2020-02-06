import { Injectable } from '@angular/core';
import { HttpLayerService } from './http-layer.service';
import { Observable, BehaviorSubject } from 'rxjs';
import { AssetResult, Asset } from '../models/asset';
import { map } from 'rxjs/operators';
import * as _ from 'lodash';
import { Country } from '../models/country';
import { MimeTypeData } from '../models/mime-type';

@Injectable({
  providedIn: 'root'
})
export class AssetsService {
  private url = 'asset';

  private assets: AssetResult = null;
  private assetsSubject = new BehaviorSubject<AssetResult>(this.assets);
  public assets$ = this.assetsSubject.asObservable();

  private asset: Asset = null;
  private assetSubject = new BehaviorSubject<Asset>(this.asset);
  public asset$ = this.assetSubject.asObservable();

  constructor(private httpLayer: HttpLayerService) { }

  public getAssets(pageId: number): void {
    this.httpLayer.get(`${this.url}?pageId=${pageId}`).pipe(
      map((data) => {
        return this.CreateAssetsResultObj(data);
      })
    ).subscribe((data) => {
      this.assetsSubject.next(data);
    });
  }

  public getAssetById(assetId: string): void {
    this.httpLayer.get(`${this.url}/${assetId}`).pipe(
      map((data) => {
        return this.CreateAssetObj(data);
      })
    ).subscribe((data) => {
      this.assetSubject.next(data);
    });
  }

  public CreateAsset(asset: Asset): Observable<Asset> {
    return this.httpLayer.post(`${this.url}`, asset);
  }

  public UpdateAsset(assetId: string, asset: Asset): Observable<Asset> {
    return this.httpLayer.put(`${this.url}/${assetId}`, asset);
  }

  public DeleteAsset(assetId: string): Observable<Asset> {
    return this.httpLayer.delete(`${this.url}/${assetId}`);
  }

  public CreateAssetsResultObj(resultObj: any): AssetResult {
    const assets = _.map(resultObj.data, (dataObj: Asset) => {
      return this.CreateAssetObj(dataObj);
    });
    return new AssetResult(assets, resultObj.totalPages, resultObj.currentPage);
  }

  public CreateAssetObj(dataObj: any): Asset {
    const country = dataObj.Country ? new Country(dataObj.Country.CountryId, dataObj.Country.CountryName) : null;
    const mimeType = dataObj.MimeType ? new MimeTypeData(dataObj.MimeType.MimeTypeId, dataObj.MimeType.Type) : null;
    return new Asset(dataObj.AssetId, dataObj.FileName, dataObj.CreatedBy, dataObj.Email, dataObj.Description,
      dataObj.CreatedOn, dataObj.CountryId, dataObj.MimeTypeId, country, mimeType);
  }
}
