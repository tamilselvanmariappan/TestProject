import { Component, OnInit } from '@angular/core';
import { Asset } from 'src/app/models/asset';
import { AssetsService } from 'src/app/services/assets.service';
import { Country } from 'src/app/models/country';
import { MimeTypeData } from 'src/app/models/mime-type';

@Component({
  selector: 'app-asset-create',
  templateUrl: './asset-create.component.html',
  styleUrls: ['./asset-create.component.scss']
})
export class AssetCreateComponent implements OnInit {
  public assetData: Asset;
  public countryData: string;
  public mimeTypeData: string;

  constructor(private assetsService: AssetsService) { }

  ngOnInit() {
    this.assetData = this.assetsService.CreateAssetObj({});
  }

  public handleFormSubmit(): void {
    const country = new Country(null, this.countryData);
    this.assetData.country = country;
    const mimeData = new MimeTypeData(null, this.mimeTypeData);
    this.assetData.MimeType = mimeData;
    this.assetsService.CreateAsset(this.assetData).subscribe((data) => {
      this.assetData = this.assetsService.CreateAssetObj({});
      alert('Data Saved successully!');
    });
  }
}
